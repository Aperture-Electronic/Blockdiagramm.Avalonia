using Avalonia;
using Avalonia.Controls;
using Avalonia.Logging;
using Blockdiagramm.Extensions;
using Blockdiagramm.ViewModels.Diagram.Component;
using DynamicData;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring.Router
{
    public class AStarRouter
    {
        public RouterConfiguration Configuration { get; set; }

        private readonly Canvas canvas;
        private List<Rect> rectObstacles = null!;
        private readonly Dictionary<OrthogonalDirection, SortedList<double, SortedList<double, OrthogonalSegment>>> lineObstacles
            = new()
            {
                { OrthogonalDirection.Vertical, new() },
                { OrthogonalDirection.Horizontal, new() }
            };

        /// <summary>
        /// Update the obstacles for the router
        /// </summary>
        public void UpdateObstacles()
        {
            var children = canvas.Children;
            rectObstacles = children.Where(obj => obj is IRectObstacle).Cast<IRectObstacle>()
                .Where(rect => rect.IsObstacleValid)
                .Select(rect => rect.BoundBox.Deflate(Configuration.BoundBoxDeflate)).ToList();

            // Add the segments
            lineObstacles[OrthogonalDirection.Horizontal].Clear();
            lineObstacles[OrthogonalDirection.Vertical].Clear();

            // Create the segments
            var lineObstacleItems = children.Where(obj => obj is ILineObstacle).Cast<ILineObstacle>().Where(l => l.IsObstacleValid);
            var segments = lineObstacleItems.SelectMany((l) => l.Segments);
            AddSegments(segments);
        }

        private void AddSegments(IEnumerable<OrthogonalSegment> segments)
        {
            var segmentGroup = from segment in segments group segment by segment.Direction into g select g;
            
            foreach (var group in segmentGroup)
            {
                if (group.Key == OrthogonalDirection.NotOrthogonal) continue;
                var targetGroup = lineObstacles[group.Key];

                foreach (var segment in group)
                {
                    if (targetGroup.TryGetValue(segment.CoordinateA, out SortedList<double, OrthogonalSegment>? value))
                    {
                        value.Add(segment.CoordinateBMin, segment);
                    }
                    else
                    {
                        // List<OrthogonalSegment> segmentsOfSameCoorA = new() { segment };
                        SortedList<double, OrthogonalSegment> segmentsOfSameCoorA = new(1)
                        {
                            { segment.CoordinateBMin, segment }
                        };
                        targetGroup.Add(segment.CoordinateA, segmentsOfSameCoorA);
                    }
                }
            }
        }

        public void AddLineObstacle(ILineObstacle lineObstacle)
        {
            var segments = lineObstacle.Segments;
            AddSegments(segments);
        }

        /// <summary>
        /// Check if the point (and segment by relative point) conflict 
        /// to any rectangle bound, or any other segment
        /// </summary>
        /// <param name="point">Point to check</param>
        /// <param name="relative">Segment relative point</param>
        /// <returns>If any point (or segment) conflicted, return true</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool CheckConflict(Point point, Point relative)
        {
            bool hasRectConflict = rectObstacles.Any((o) => o.Contains(point));
            if (hasRectConflict)
            {
                return true;
            }

            // Check if the segment of neighbor point and relative point
            // conflict with any existing segment
            OrthogonalSegment segment = new(point, relative);
            var targetSegmentSet = lineObstacles[segment.Direction];
            //if (targetSegmentSet.TryGetValue(segment.CoordinateA, out LinkedList<OrthogonalSegment>? targetSubset))
            if (targetSegmentSet.TryGetValue(segment.CoordinateA, out SortedList<double, OrthogonalSegment>? targetSubset))
            {
                foreach (var other in targetSubset)
                {
                    if (segment.IsOverOrthogonalSegment(other.Value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private IEnumerable<(Point, double)> GetNeighbor(Point point)
        {
            var neighbor = point.GetOrthogonalNeighbor(Configuration.Step);
            
            foreach (var n in neighbor)
            {
                // Check if the neighbor point conflict with any rectangle bound
                // Check if the segment of neighbor point and relative point conflict
                if(CheckConflict(n, point))
                {
                    continue;
                }

                // Count the cross point to any existing segment
                // Each cross point will take an additional cost
                OrthogonalSegment segment = new(n, point);
                var targetSegmentSet = lineObstacles[segment.Direction == OrthogonalDirection.Horizontal ? 
                    OrthogonalDirection.Vertical : OrthogonalDirection.Horizontal];
                int crossCount = 0;
                int setCount = targetSegmentSet.Count;

                // Get the span
                // The lower index is the index of smallest number greater than Bmin
                // The upper index is the index of largest number less than Bmax
                int lowerIndex = targetSegmentSet.Keys.BinarySearch(segment.CoordinateBMin);
                int upperIndex = targetSegmentSet.Keys.BinarySearch(segment.CoordinateBMax);
                lowerIndex = lowerIndex < 0 ? (~lowerIndex) : lowerIndex;
                upperIndex = upperIndex < 0 ? (~upperIndex) - 1 : upperIndex;
                upperIndex = upperIndex >= setCount ? setCount - 1 : upperIndex;

                // For each segments in the span
                for (int i = lowerIndex; i <= upperIndex; i++)
                {
                    var segmentSubset = targetSegmentSet.GetValueAtIndex(i);
                    int subsetCount = segmentSubset.Count;

                    var foundIndex = segmentSubset.Keys.BinarySearch(segment.CoordinateA);
                    foundIndex = foundIndex < 0 ? ~foundIndex - 1 : foundIndex;
                    if ((foundIndex >= 0) && (foundIndex < subsetCount))
                    { 
                        var node = segmentSubset.GetValueAtIndex(foundIndex);
                        if (node.CoordinateBMax >= segment.CoordinateA)
                        {
                            crossCount++;
                        }
                    }
                }

                double additionalCost = crossCount * Configuration.CrossCost;

                yield return (n, additionalCost);
            }
        }

        public AStarRouter(Canvas canvas, RouterConfiguration? configuration = null)
        {
            this.canvas = canvas;
            Configuration = configuration ?? new();
        }

        private IEnumerable<Point> GetRoute(Point from, Point to, Func<Point, Point, double> distanceFunction,
            RouteDirection initialDirection)
        {
            AStarNode currentNode = new(from, initialDirection: initialDirection);

            // SimplePriorityQueue<AStarNode> openList = new();
            PriorityQueue<AStarNode, double> openList = new();
            List<AStarNode> closeList = new();

            currentNode.UpdateG(from, distanceFunction);
            currentNode.UpdateH(to, distanceFunction);

            // openList.Add(currentNode);
            openList.Enqueue(currentNode, 0);

            do
            {
                // Set the minimum cost node to current
                currentNode = openList.Dequeue();

                // Delete the node from open node list and add it to close node list
                closeList.Add(currentNode);

                // If the close node list contains target
                if (closeList.Any((e) => e.Value.Equals(to)))
                {
                    break;
                }

                var neighbors = GetNeighbor(currentNode.Value);
                var nodes = AStarNode.CreateNodes(neighbors);

                int iteration = 0;

                foreach (var node in nodes)
                {
                    if (closeList.Any(e => e.Value.Equals(node.Value))) 
                    {
                        continue;
                    }

                    if (!openList.UnorderedItems.Any(e => e.Element.Equals(node.Value)))
                    {
                        node.SetFather(currentNode);

                        // Calculate the turn loss
                        double turnCost = (node.DirectonFromFather != currentNode.DirectonFromFather) ? 
                            Configuration.TurnCost : 0;

                        // Update the value of G and H with additional loss
                        node.UpdateG(distanceFunction, additionalCost: turnCost);
                        node.UpdateH(to, distanceFunction);

                        // Enqueue the node by priority equals its cost (F = G + H)
                        openList.Enqueue(node, node.F);
                    }
                    else if (node.F > currentNode.G + node.H)
                    {
                        node.SetFather(currentNode);
                        node.UpdateG(distanceFunction);
                    }

                    iteration++;
                    if (iteration > Configuration.MaximumIteration)
                    {
                        throw new Exception($"Can not find a route between points {from} and {to}, tried {iteration} times");
                    }
                }
            // } while (openList.Any())
            } while (openList.Count > 0);

            var endPointQueue = closeList.Where((e) => e.Value.Equals(to));
            if (endPointQueue.Any())
            {
                currentNode = endPointQueue.First();

                while ((currentNode != null))
                {
                    yield return currentNode.Value;
                    if (currentNode.FatherNode != null)
                    {
                        currentNode = currentNode.FatherNode;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            yield break;
        }
    
        public IEnumerable<Point> GetRoute(Point fromPort, Point toPort, 
            PortDirection fromDirection, PortDirection toDirection)
        {
            // Fanout the port
            Point startPointFanout = fromPort.SnapFanout(fromDirection, Configuration.PortFanout, Configuration.Step);
            Point endPointFanout = toPort.SnapFanout(toDirection, Configuration.PortFanout, Configuration.Step);

            // Get initial route direction by port direction
            RouteDirection initialDirection = fromDirection == PortDirection.Slave ?
                RouteDirection.West : RouteDirection.East;

            // Get the route from start fanout to end fanout
            var route = GetRoute(startPointFanout, endPointFanout, (a, b) => a.ManhattenTo(b), initialDirection);

            // Reverse the route to output
            var routeReversed = route.Reverse();    

            // Return start port
            yield return fromPort;

            // Return the route
            foreach (var point in routeReversed)
            {
                yield return point;
            }

            // Return end port
            yield return toPort;
        }
    }
}

using Avalonia;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring.Router
{
    public class AStarNode
    {
        public Point Value { get; private set; }
        public AStarNode? FatherNode { get; private set; }

        private RouteDirection InitialDirection { get; }

        public RouteDirection DirectonFromFather
            => FatherNode == null ? InitialDirection : FatherNode.Value.VectorDirection(Value);

        /// <summary>
        /// The cost from start node to current node
        /// </summary>
        public double G { get; private set; }

        /// <summary>
        /// Estimated cost from current node to target node
        /// </summary>
        public double H { get; private set; }

        /// <summary>
        /// The additional cost of this node
        /// </summary>
        public double E { get; }

        public double F => G + H + E;

        /// <summary>
        /// Update and get H by the target value for A* algorithm
        /// </summary>
        /// <param name="target">Pointarget node</param>
        /// <param name="distanceFunction">Distance function, usually using Manhattan function</param>
        public double UpdateH(Point target,
            Func<Point, Point, double> distanceFunction)
            => H = distanceFunction(Value, target);

        /// <summary>
        /// Update and get G by a start coordinate for A* algorithm
        /// </summary>
        /// <param name="start">Start node</param>
        /// <param name="distanceFunction">Distance function, usually using Manhattan function</param>
        public double UpdateG(Point start,
            Func<Point, Point, double> distanceFunction)
            => G = distanceFunction(start, Value);

        /// <summary>
        /// Update and get G by a start coordinate for A* algorithm
        /// </summary>
        /// <param name="start">Start node</param>
        /// <param name="distanceFunction">Distance function, usually using Manhattan function</param>
        public double UpdateG(AStarNode start, Func<Point, Point, double> distanceFunction)
            => UpdateG(start.Value, distanceFunction);

        /// <summary>
        /// Update and get G by the father node (which has been setted)
        /// </summary>
        /// <param name="distanceFunction">Distance function, usually using Manhattan function</param>
        public double UpdateG(Func<Point, Point, double> distanceFunction, double additionalCost = 0)
        {
            if (FatherNode == null)
            {
                G = 0;
                return 0;
            }

            return G = FatherNode.G + distanceFunction(FatherNode.Value, Value) + additionalCost;
        }

        /// <summary>
        /// Set the father node
        /// </summary>
        public void SetFather(AStarNode node) => FatherNode = node;

        public AStarNode(Point node, 
            AStarNode? fatherNode = null, 
            RouteDirection initialDirection = RouteDirection.North,
            double additionCost = 0)
        {
            Value = node;
            FatherNode = fatherNode;
            InitialDirection = initialDirection;
            E = additionCost;
        }

        public static IEnumerable<AStarNode> CreateNodes(IEnumerable<Point> points)
            => points.Select((p) => new AStarNode(p));

        public static IEnumerable<AStarNode> CreateNodes(IEnumerable<(Point, double)> points)
            => points.Select(((Point point, double cost) p) => new AStarNode(p.point, additionCost: p.cost));

        public override string ToString() => $"{Value}, H = {H}, G = {G}, F = {F}";
    }
}

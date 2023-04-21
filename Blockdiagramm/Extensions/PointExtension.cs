using Avalonia;
using Blockdiagramm.Models;
using Blockdiagramm.Renderer.Wiring.Router;
using DynamicData.Aggregation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class PointExtension
    {
        public static OrthogonalDirection CheckOrthogonal(this Point point, Point relative)
            => false switch
            {
                _ when point.X == relative.X => OrthogonalDirection.Vertical,
                _ when point.Y == relative.Y => OrthogonalDirection.Horizontal,
                _ => OrthogonalDirection.NotOrthogonal
            };

        public static (double, double) ToTuple(this Point point) => (point.X, point.Y);

        public static double Det(this Point point, Point other) => (point.X * other.Y) - (point.Y * other.X);

        public static bool IsOnLine(this Point p1, Point p2, Point p3, double threshold = double.Epsilon)
        {
            Point vectorP12 = p2 - p1;
            Point vectorP23 = p3 - p2;
            double point = vectorP12.Det(vectorP23);

            return Math.Abs(point) <= threshold;
        }

        public static bool IsOnOrthogonalSegment(this Point point, Point start, Point end)
        {
            if (start.X == end.X) // Vertical
            {
                if (start.Y < end.Y)
                {
                    return (start.Y <= point.Y) && (end.Y >= point.Y);
                }
                else
                {
                    return (end.Y <= point.Y) && (start.Y >= point.Y);
                }
            }
            else if (start.Y == end.Y) // Horizontal
            {
                if (start.X < end.X)
                {
                    return (start.X <= point.X) && (end.X >= point.X);
                }
                else
                {
                    return (end.X <= point.X) && (start.X >= point.X);
                }
            }

            throw new Exception("The segment is not orthogonal");
        }

        public static bool IsOnSegment(this Point point, Point start, Point end, double threshold = double.Epsilon)
        {
            if (point.IsOnLine(start, end, threshold))
            {
                (double minX, double maxX) = (start.X, end.X).MinMax();
                (double minY, double maxY) = (start.Y, end.Y).MinMax();

                if ((point.X >= minX) && (point.X <= maxX) &&
                    (point.Y >= minY) && (point.Y <= maxY))
                {
                    return true;
                }
            }

            return false;
        }

        public static bool IsOrthogonalSegmentOverOrthogonalSegment(this Point point, Point relative, Point start, Point end)
        {
            var orthA = point.CheckOrthogonal(relative);
            var orthB = start.CheckOrthogonal(end);

            if ((orthA == OrthogonalDirection.NotOrthogonal) || (orthB == OrthogonalDirection.NotOrthogonal))
            {
                throw new Exception("At least one of the segment is not orthogonal");
            }

            if (orthA != orthB)
            {
                return false;
            }

            if ((orthA == OrthogonalDirection.Vertical) && (point.X == start.X))
            {
                if (start.Y < end.Y)
                {
                    bool pointOverSegment = (point.Y >= start.Y) && (point.Y <= end.Y);
                    bool relativeOverSegment = (relative.Y >= start.Y) && (relative.Y <= end.Y);
                    return pointOverSegment || relativeOverSegment;
                }
                else
                {
                    bool pointOverSegment = (point.Y >= end.Y) && (point.Y <= start.Y);
                    bool relativeOverSegment = (relative.Y >= end.Y) && (relative.Y <= start.Y);
                    return pointOverSegment || relativeOverSegment;
                }
            }
            else if ((orthA == OrthogonalDirection.Horizontal) && (point.Y == start.Y))
            {
                if (start.X < end.X)
                {
                    bool pointOverSegment = (point.X >= start.X) && (point.X <= end.X);
                    bool relativeOverSegment = (relative.X >= start.X) && (relative.X <= end.X);
                    return pointOverSegment || relativeOverSegment;
                }
                else
                {
                    bool pointOverSegment = (point.X >= end.X) && (point.X <= start.X);
                    bool relativeOverSegment = (relative.X >= end.X) && (relative.X <= start.X);
                    return pointOverSegment || relativeOverSegment;
                }
            }

            return false;
        }

        public static bool IsSegmentOverSegment(this Point point, Point relative, Point start, Point end, double threshold = double.Epsilon)
        {
            if (!point.IsOnLine(start, end, threshold))
            {
                return false;
            }

            if (!relative.IsOnLine(start, end, threshold))
            {
                return false;
            }

            (double minX, double maxX) = (start.X, end.X).MinMax();
            (double minY, double maxY) = (start.Y, end.Y).MinMax();

            bool pointOverSegment = ((point.X >= minX) && (point.X <= maxX) &&
                    (point.Y >= minY) && (point.Y <= maxY));
            bool relativeOverSegment = ((relative.X >= minX) && (relative.X <= maxX) &&
                    (relative.Y >= minY) && (relative.Y <= maxY));

            return pointOverSegment || relativeOverSegment;
        }

        public static bool IsStraddle(this Point point, Point relative, Point start, Point end, double threshold = double.Epsilon)
        {
            Point v1 = start - relative;
            Point v2 = end - relative;
            Point vm = point - relative;

            return Det(v1, vm) * Det(v2, vm) <= threshold;
        }

        public static bool IsCrossSegment(this Point point, Point relative, Point start, Point end, double threshold = double.Epsilon)
        {
            // Fast cross detection
            (double minX1, double maxX1) = (point.X, relative.X).MinMax();
            (double minY1, double maxY1) = (point.Y, relative.Y).MinMax();
            (double minX2, double maxX2) = (start.X, end.X).MinMax();
            (double minY2, double maxY2) = (start.Y, end.Y).MinMax();

            if ((maxX1 < minX2) || (maxY1 < minY2) || (maxX2 < minX1) || (maxY2 < minY1))
            {
                return false;
            }

            // Straddle detection
            if (point.IsStraddle(relative, start, end, threshold) &&
                end.IsStraddle(start, relative, end, threshold))
            {
                return true;
            }

            return false;
        }

        public static bool NearTo(this Point point, Point other, double thresholdX, double thresholdY)
            => point.IsOnHorizontalLine(other, thresholdX) && point.IsOnVerticalLine(other, thresholdY);

        public static bool IsOnHorizontalLine(this Point point, Point other, double threshold = 0)
            => threshold == 0 ? point.X == other.X : Math.Abs(point.X - other.X) <= threshold;

        public static bool IsOnVerticalLine(this Point point, Point other, double threshold = 0)
            => threshold == 0 ? point.Y == other.Y : Math.Abs(point.Y - other.Y) <= threshold;

        public static IEnumerable<Point> GetOrthogonalNeighbor(this Point point, double distance)
        {
            Point xUnit = new(distance, 0);
            Point yUnit = new(0, distance);

            yield return point + xUnit;
            yield return point - xUnit;
            yield return point + yUnit;
            yield return point - yUnit;
        }

        public static double ManhattenTo(this Point point, Point other) => Math.Abs(point.X - other.X) + Math.Abs(point.Y - other.Y);

        public static Point Fanout(this Point point, PortDirection direction, double distance)
            => direction switch
            {
                PortDirection.Master => point.WithX(point.X + distance),
                PortDirection.Slave => point.WithX(point.X - distance),
                _ => throw new Exception("Unknown fanout direction")
            };

        public static Point SnapFanout(this Point point, PortDirection direction, double distance, double snapTo)
        {
            Point fanout = point.Fanout(direction, distance);
            Point snap = GridSnap.SnapToRectGrid(fanout, snapTo);

            return direction switch
            {
                PortDirection.Slave when point.X - snap.X < distance => snap.WithX(snap.X - snapTo),
                PortDirection.Master when snap.X - point.X < distance => snap.WithX(snap.X + snapTo),
                _ => snap
            };
        }

        public static RouteDirection VectorDirection(this Point point, Point to)
            => false switch
            {
                _ when (to.X > point.X) => RouteDirection.East,
                _ when (to.X < point.X) => RouteDirection.West,
                _ when (to.Y > point.Y) => RouteDirection.North,
                _ when (to.Y < point.Y) => RouteDirection.South,
                _ => throw new Exception("Same point has not vector direction")
            };
    }
}

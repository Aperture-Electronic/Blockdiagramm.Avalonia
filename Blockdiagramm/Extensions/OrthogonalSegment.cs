using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public readonly struct OrthogonalSegment : IComparable<OrthogonalSegment>
    {
        public double CoordinateA { get; }
        public double CoordinateBMin { get; }
        public double CoordinateBMax { get; }

        public OrthogonalDirection Direction { get; }

        public OrthogonalSegment(Point start, Point end)
        {
            Direction = end.CheckOrthogonal(start);
            if (Direction == OrthogonalDirection.NotOrthogonal)
            {
                throw new Exception("Invalid start and end point for a orthogonal segment");
            }

            if (Direction == OrthogonalDirection.Horizontal)
            {
                CoordinateA = start.Y;
                if (start.X > end.X)
                {
                    CoordinateBMax = start.X;
                    CoordinateBMin = end.X;
                }
                else
                {
                    CoordinateBMax = end.X;
                    CoordinateBMin = start.X;
                }
            }
            else
            {
                CoordinateA = start.X;
                if (start.Y > end.Y)
                {
                    CoordinateBMax = start.Y;
                    CoordinateBMin = end.Y;
                }
                else
                {
                    CoordinateBMax = end.Y;
                    CoordinateBMin = start.Y;
                }
            }
        }
    
        public Segment ToSegment()
        {
            if (Direction == OrthogonalDirection.Vertical)
            {
                return new Segment(new Point(CoordinateA, CoordinateBMin), new Point(CoordinateA, CoordinateBMax));
            }
            else
            {
                return new Segment(new Point(CoordinateBMin, CoordinateA), new Point(CoordinateBMax, CoordinateA));
            }
        }

        public bool IsOverOrthogonalSegment(OrthogonalSegment other)
        {
            if (Direction != other.Direction) return false;

            if (CoordinateA == other.CoordinateA)
            {
                bool minOver = (CoordinateBMin >= other.CoordinateBMin) && (CoordinateBMin <= other.CoordinateBMax);
                bool maxOver = (CoordinateBMax >= other.CoordinateBMin) && (CoordinateBMax <= other.CoordinateBMax);
                return minOver || maxOver;
            }

            return false;
        }

        public bool IsCross(OrthogonalSegment other)
        {
            if (Direction == other.Direction) return false;

            bool aCrossB = (CoordinateBMin <= other.CoordinateA) && (CoordinateBMax >= other.CoordinateA);
            bool bCrossA = (other.CoordinateBMin <= CoordinateA) && (other.CoordinateBMax >= CoordinateA);
            return aCrossB && bCrossA;
        }

        public bool HalfCross(OrthogonalSegment other)
            => (CoordinateBMin <= other.CoordinateA) && (CoordinateBMax >= other.CoordinateA);
        public int CompareTo(OrthogonalSegment other) => CoordinateBMin.CompareTo(other.CoordinateBMin);
    }
}

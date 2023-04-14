using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public readonly struct Segment
    {
        public Point Start { get; }
        public Point End { get; }

        public Segment(Point start, Point end)
        {
            Start = start;
            End = end;
        }

        public OrthogonalDirection Orthogonal
            => End.CheckOrthogonal(Start);
    }
}

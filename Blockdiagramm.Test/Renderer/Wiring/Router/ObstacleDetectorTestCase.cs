using Avalonia;
using Avalonia.Controls;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring.Router;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Test.Renderer.Wiring.Router
{
    public class ObstacleDetectorTestCase : ITestCase<int>
    {
        public Point Point { get; }
        public Point Relative { get; }

        public List<OrthogonalSegment> Segments { get; }

        public ObstacleDetectorTestCase(Point point, Point relative, List<OrthogonalSegment> segments)
        {
            Point = point;
            Relative = relative;
            Segments = segments;
        }

        public int CorrectResult
        {
            get
            {
                var nSegments = Segments.Select(s => s.ToSegment());
                var result = nSegments.Where(s => Point.IsCrossSegment(Relative, s.Start, s.End));
                return result.Count();
            }
        }

        public int SegmentCount => Segments.Count;
    }
}

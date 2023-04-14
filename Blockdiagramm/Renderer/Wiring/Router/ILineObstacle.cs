using Avalonia;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring.Router
{
    public interface ILineObstacle
    {
        public bool IsObstacleValid { get; }
        //public bool IsOnLine(Point point, double threshold = double.Epsilon);
        //public bool IsOverSegment(Point point, Point relative, double threshold = double.Epsilon);
        //public bool IsCrossSegment(Point point, Point relative, double threshold = double.Epsilon);

        public IEnumerable<OrthogonalSegment> Segments { get; }
    }
}

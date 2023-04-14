using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring.Router
{
    public interface IRectObstacle
    {
        public bool IsObstacleValid { get; }

        public Rect BoundBox { get; }
    }
}

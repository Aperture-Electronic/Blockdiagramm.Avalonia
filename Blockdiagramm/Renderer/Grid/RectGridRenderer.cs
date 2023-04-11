using Avalonia.Media;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Grid
{
    public class RectGridRenderer : IGridRenderer
    {
        public IPen GridPen { get; set; }

        public double StartOffsetX { get; set; }
        public double StartOffsetY { get; set; }
        public double Width { get; set; }
        public double Height { get; set; }

        public double EndOffsetX => StartOffsetX + Width;
        public double EndOffsetY => StartOffsetY + Height;

        public double IntervalX { get; set; }
        public double IntervalY { get; set; }

        public RectGridRenderer(IPen gridPen) => GridPen = gridPen;

        public void Render(DrawingContext context)
        {
            // Draw horizontal lines
            for (double x = StartOffsetX; x < EndOffsetX; x += IntervalX) 
            {
                context.DrawLine(GridPen, (x, StartOffsetY).ToPoint(), (x, EndOffsetY).ToPoint());
            }

            // Draw vertical lines
            for (double y = StartOffsetY; y < EndOffsetY; y += IntervalY)
            {
                context.DrawLine(GridPen, (StartOffsetX, y).ToPoint(), (EndOffsetX, y).ToPoint());
            }
        }
    }
}

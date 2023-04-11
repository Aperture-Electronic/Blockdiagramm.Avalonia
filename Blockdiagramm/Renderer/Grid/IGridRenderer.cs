using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Grid
{
    public interface IGridRenderer
    {
        public IPen GridPen { get; }

        public void Render(DrawingContext context);
    }
}

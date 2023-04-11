using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Blockdiagramm.Extensions;
using System.Linq;

namespace Blockdiagramm.Controls.Diagram.Wire
{
    public partial class VertexWire : UserControl
    {
        public static readonly DirectProperty<VertexWire, AvaloniaList<Point>> VerticesProperty =
            AvaloniaProperty.RegisterDirect<VertexWire, AvaloniaList<Point>>(nameof(Vertices), o => o.Vertices);

        public AvaloniaList<Point> Vertices => dplWire.Points;

        public VertexWire()
        {
            InitializeComponent();
        }
    
        public void ReduceVertices()
        {
            int verticesCount = Vertices.Count;

            for (int i = 2; i < verticesCount;)
            {
                // Delete if 3 vertices are on the same line
                if (Vertices[i - 1].IsOnLine(Vertices[i - 2], Vertices[i]))
                {
                    Vertices.RemoveAt(i - 1);
                    verticesCount--;
                }
                else
                {
                    i++;
                }
            }
        }

        public Point StartVertex => Vertices.First();
        public Point EndVertex => Vertices.Last();
    }
}

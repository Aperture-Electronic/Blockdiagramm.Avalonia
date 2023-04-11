using Avalonia.Controls;
using Avalonia.Media;
using Blockdiagramm.Renderer.Grid;

namespace Blockdiagramm.Controls.Diagram.Background
{
    public partial class DiagramGrid : UserControl
    {
        public DiagramGrid()
        {
            InitializeComponent();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            IPen pen = new Pen(Brushes.Gray, dashStyle: DashStyle.DashDot);
            RectGridRenderer gridRenderer = new(pen)
            {
                StartOffsetX = 0,
                StartOffsetY = 0,
                Width = Width, Height = Height,
                IntervalX = 20,
                IntervalY = 20,
            };
            gridRenderer.Render(context);
        }
    }
}

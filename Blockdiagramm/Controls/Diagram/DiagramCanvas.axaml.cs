using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Blockdiagramm.Controls.Diagram.Component;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System.Collections.Generic;

namespace Blockdiagramm.Controls.Diagram
{
    public partial class DiagramCanvas : UserControl
    {
        private double canvasMaxHeight = 0;
        private double canvasMaxWidth = 0;

        public DiagramCanvas()
        {
            InitializeComponent();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            // Set canvas width
            if (Bounds.Width > canvasMaxWidth)
            {
                canvas.Width = Bounds.Width;
                canvasMaxWidth = Bounds.Width;
            }

            if (Bounds.Height > canvasMaxHeight)
            {
                canvas.Height = Bounds.Height;
                canvasMaxHeight = Bounds.Height;
            }

            // Set the grid to match the canvas
            bgGrid.Width = canvasMaxWidth;
            bgGrid.Height = canvasMaxHeight;
        }

        private bool isWiring = false;
        private VertexWire wiringWire = null!;

        private void OnCanvasPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (isWiring && (wiringWire != null))
            {
                Point pointerPosition = e.GetCurrentPoint(canvas).Position;
                Point snapPosition = GridSnap.SnapToRectGrid(e.GetCurrentPoint(canvas).Position, 10);
                wiringWire.Vertices.Add(snapPosition);
            }
        }

        private void OnPortPressed(object sender, ComponentPortPressedEventArgs e)
        {
            if (sender is ComponentPart part)
            {
                if (part.RenderTransform is TranslateTransform partTransform)
                {
                    Point transformPoint = (partTransform.X, partTransform.Y).ToPoint();
                    Point canvasPoint = (Canvas.GetLeft(part).NaNAsZero(), Canvas.GetTop(part).NaNAsZero()).ToPoint();
                    Point portPoint = transformPoint + canvasPoint + e.OnComponentPosition;

                    if (isWiring && (wiringWire != null))
                    {
                        isWiring = false;
                        wiringWire.Vertices.Add(portPoint);
                        wiringWire.ReduceVertices();

                        if (wiringWire.DataContext is WireModel wm)
                        {
                            wm.WireStatus = WireStatus.Normal;
                        }
                    }
                    else
                    {
                        isWiring = true;
                        wiringWire = new();
                        var wm = new WireModel();
                        wiringWire.DataContext = wm;
                        wm.WireStatus = WireStatus.Temporary;
                        wm.WireType = WireType.Single;
                        wiringWire.Vertices.Add(portPoint);
                        canvas.Children.Add(wiringWire);
                    }
                }
            }
        }
    }
}

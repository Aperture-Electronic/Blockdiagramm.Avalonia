using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Remote.Protocol.Input;
using Blockdiagramm.Controls.Diagram.Component;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Models.Diagram;
using Blockdiagramm.Renderer.Wiring;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public partial class DiagramCanvas : UserControl
    {
        private double canvasMaxHeight = 0;
        private double canvasMaxWidth = 0;

        private readonly List<WireConnection> connections = new();
        private PortOnPart connectFrom;
        private readonly WiringManager wiringManager;

        public DiagramCanvas()
        {
            InitializeComponent();
            
            wiringManager = new(canvas);
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

        private void OnCanvasPointerPressed(object sender, PointerPressedEventArgs e)
        {
            PointerPoint currentPoint = e.GetCurrentPoint(canvas);

            if (currentPoint.Properties.IsLeftButtonPressed)
            {
                if (wiringManager.IsWiring)
                {
                    wiringManager.CancelWiring();
                }
            }
        }

        private void OnCanvasPointerMoved(object sender, PointerEventArgs e)
        {
            if (wiringManager.IsWiring)
            {
                // Get the position
                Point position = e.GetPosition(canvas);
                wiringManager.GuideWiring(position);
            }
        }

        private void OnComponentMoved(object sender, ComponentMovedEventArgs e)
        {
            if (sender is ComponentPart part)
            {
                // Get the translation delta of the part
                Point delta = e.NewPosition - e.PreviousPosition;

                // Query all connections for this part
                var connsRelatedToPart = from conn in connections
                                      where conn.IsRelatedTo(part)
                                      select conn;

                // Set these connections to temp
                part.IsObstacleValid = false;
                connsRelatedToPart.Apply(conn => conn.WireModel.WireStatus = WireStatus.Temporary);

                // Re-route these connections
                bool firstConnect = true;
                foreach (WireConnection conn in connsRelatedToPart)
                {
                    if (!conn.TranslateWire(wiringManager, delta, updateObstacles: firstConnect))
                    {
                        // Need re-route
                        part.IsObstacleValid = true;
                        conn.RerouteWire(wiringManager);
                        part.IsObstacleValid = false;

                        // After reroute, we need add new wire to the obstacles list
                        wiringManager.AddWireAsObstacle(conn.Wire);

                        // Set re-routed connection to normal
                        conn.WireModel.WireStatus = WireStatus.Normal;
                    }                

                    firstConnect = false;
                }

                // Set these connections to normal
                connsRelatedToPart.Apply(conn => conn.WireModel.WireStatus = WireStatus.Normal);
                part.IsObstacleValid = true;
            }
        }

        private void OnPortPressed(object sender, ComponentPortPressedEventArgs e)
        {
            if (sender is ComponentPart part)
            {
                if (part.RenderTransform is TranslateTransform partTransform)
                {
                    Point portPoint = part.GetPortPosition(e.Port);

                    if (wiringManager.IsWiring)
                    {
                        var wire = wiringManager.CompleteWiring(portPoint, e.PortModel.Direction);

                        // Make new connection
                        PortOnPart connectTo = new(part, e.Port);
                        WireConnection connection = new(connectFrom, connectTo, wire);
                        connections.Add(connection);
                    }
                    else
                    {
                        connectFrom = new(part, e.Port);
                        wiringManager.StartWiring(portPoint, e.PortModel.Direction);
                    }
                }
            }
        }
    }
}

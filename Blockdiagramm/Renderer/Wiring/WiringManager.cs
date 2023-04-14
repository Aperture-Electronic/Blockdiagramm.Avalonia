using Avalonia;
using Avalonia.Controls;
using Blockdiagramm.Controls.Diagram.Component;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring.Router;
using Blockdiagramm.ViewModels.Diagram.Component;
using Blockdiagramm.ViewModels.Diagram.Wire;
using HarfBuzzSharp;
using SkiaSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring
{
    public class WiringManager
    {
        public bool IsWiring { get; private set; }

        private PortDirection startDirection;

        public VertexWire WiringWire { get; private set; } = null!;
        private WireModel wiringWireModel = null!;

        private readonly Canvas canvas;

        public bool CheckConflict(VertexWire wire)
        {
            AStarRouter router = new(canvas);
            router.UpdateObstacles();

            int count = wire.Vertices.Count;

            for (int i = 1; i < count - 1; i++)
            {
                if (router.CheckConflict(wire.Vertices[i], wire.Vertices[i - 1]))
                {
                    return true;
                }
            }

            return false;
        }

        public void Route(VertexWire wire, Point startPoint, Point endPoint,
            PortDirection startDirection, PortDirection endDirection)
        {
            AStarRouter router = new(canvas);
            router.Configuration.TurnCost = 50;
            router.Configuration.CrossCost = 100;

            router.UpdateObstacles();
            var route = router.GetRoute(startPoint, endPoint, startDirection, endDirection);

            wire.Vertices.Clear();
            wire.Vertices.AddRange(route);
            wire.ReduceVertices();
        }

        public WiringManager(Canvas canvas) => this.canvas = canvas;    

        public void StartWiring(Point startPoint, PortDirection direction)
        {
            IsWiring = true;
            startDirection = direction;

            WiringWire = new();
            wiringWireModel = new()
            {
                WireStatus = WireStatus.Temporary,
                WireType = WireType.Single // TODO
            };

            WiringWire.DataContext = wiringWireModel;
            WiringWire.IsHitTestVisible = false;
            WiringWire.Vertices.Add(startPoint);

            canvas.Children.Add(WiringWire);
        }

        public VertexWire CompleteWiring(Point endPoint, PortDirection direction)
        {
            if (!IsWiring)
            {
                throw new Exception("Can not complete wiring until the wiring mode");
            }

            IsWiring = false;

            // Fanout the port for router
            Point startPoint = WiringWire.Vertices[0];

            // Use router to route
            Route(WiringWire, startPoint, endPoint, startDirection, direction);

            // Set wire to normal
            wiringWireModel.WireStatus = WireStatus.Normal;

            // Set return wire
            VertexWire wireToReturn = WiringWire;

            // Release the reference to wire
            WiringWire = null!;
            wiringWireModel = null!;

            return wireToReturn;
        }

        public void CancelWiring()
        {
            if (!IsWiring)
            {
                throw new Exception("Can not cancel wiring until the wiring mode");
            }

            canvas.Children.Remove(WiringWire);
            wiringWireModel = null!;
            WiringWire = null!;

            IsWiring = false;
        }

        public void GuideWiring(Point nextPoint)
        {
            if (!IsWiring)
            {
                throw new Exception("Can not guide wiring until the wiring mode");
            }

            if (WiringWire.Vertices.Count == 1)
            {
                WiringWire.Vertices.Add(nextPoint);
            }
            else
            {
                WiringWire.Vertices[1] = nextPoint;
            }
        }
    }
}

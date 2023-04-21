using Avalonia;
using Blockdiagramm.Controls.Diagram;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Renderer.Wiring;
using Blockdiagramm.ViewModels.Diagram;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System;

namespace Blockdiagramm.Models.Diagram
{
    public class WireConnection
    {
        public PortOnPart From { get; }
        public PortOnPart To { get; }

        public VertexWire Wire { get; }

        public WireModel WireModel
        {
            get
            {
                if (Wire.DataContext is WireModel model)
                {
                    return model;
                }

                throw new Exception("The vertex wire has not data context of wire model");
            }
        }

        public WireConnection(PortOnPart from, PortOnPart to, VertexWire wire)
        {
            From = from;
            To = to;
            Wire = wire;
        }

        public bool TranslateWire(WiringManager manager, Point delta, bool updateObstacles = false)
        {
            // Check if we can translation the wire without conflict
            if (!manager.CheckConflict(Wire, updateObstacles))
            {
                Wire.Translate(delta);

                return true;
            }
            return false;
        }

        public void RerouteWire(WiringManager manager, bool updateObstacles = false)
        {
            Point startPosition = From.Part.GetPortPosition(From.Port);
            Point endPosition = To.Part.GetPortPosition(To.Port);
            PortDirection startDirection = From.Part.GetPortDirection(From.Port);
            PortDirection endDirection = To.Part.GetPortDirection(To.Port);

            manager.Route(Wire, startPosition, endPosition, startDirection, endDirection, updateObstacles);
        }

        public bool IsRelatedTo(IPart<IPartModel<IPortModel>, IPortModel> part)
        {
            if (From.Part == part)
            {
                return true;
            }

            if (To.Part == part)
            {
                return true;
            }

            return false;
        }
    }
}

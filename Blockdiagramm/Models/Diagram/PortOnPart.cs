using Blockdiagramm.Controls.Diagram;
using Blockdiagramm.ViewModels.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Models.Diagram
{
    public readonly struct PortOnPart
    {
        public readonly IPart<IPartModel<IPortModel>, IPortModel> Part;
        public readonly IPort<IPortModel> Port;

        public PortOnPart(IPart<IPartModel<IPortModel>, IPortModel> part, IPort<IPortModel> port)
        {
            Part = part;
            Port = port;
        }
    }
}

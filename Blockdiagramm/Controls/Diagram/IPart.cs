using Avalonia;
using Blockdiagramm.ViewModels.Diagram;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public interface IPart<out TPartModel, out TPortModel> 
        where TPartModel : IPartModel<TPortModel>
        where TPortModel : IPortModel
    {
        public TPartModel? Model { get; }

        public Point GetPortPosition(IPort<IPortModel> port);

        public PortDirection GetPortDirection(IPort<IPortModel> port);
    }
}

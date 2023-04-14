using Blockdiagramm.ViewModels.Diagram;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public interface IPort<out TPortModel> where TPortModel : IPortModel
    {
        public TPortModel? Model { get; }
    }
}

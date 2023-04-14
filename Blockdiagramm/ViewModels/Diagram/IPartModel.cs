using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram
{
    public interface IPartModel<out TPortModel> where TPortModel : IPortModel
    {
        public Point GetPortPosition(IPortModel portModel, Rect partBound);
    }
}

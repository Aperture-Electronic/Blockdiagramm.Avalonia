using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public interface IWindowBaseViewModel
    {
        public Interaction<object?, object?> CloseWindow { get; }
        public Interaction<object?, object?> MaximizeWindow { get; }
        public Interaction<object?, object?> MinimizeWindow { get; }
    }
}

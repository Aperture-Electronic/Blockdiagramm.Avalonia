using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public interface IWindowBaseViewModel
    {
        public Interaction<Unit, Unit> CloseWindow { get; }
        public Interaction<Unit, Unit> MaximizeWindow { get; }
        public Interaction<Unit, Unit> MinimizeWindow { get; }
    }
}

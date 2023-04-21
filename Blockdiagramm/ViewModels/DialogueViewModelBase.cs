using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public class DialogueViewModelBase : ViewModelBase, IWindowBaseViewModel
    {
        public ICommand CloseWindowCommand { get; }

        public Interaction<Unit, Unit> CloseWindow { get; } = new();

        public Interaction<Unit, Unit> MaximizeWindow { get; } = new();

        public Interaction<Unit, Unit> MinimizeWindow { get; } = new();

        public DialogueViewModelBase()
        {
            CloseWindowCommand = ReactiveCommand.CreateFromObservable(() => CloseWindow.Handle(Unit.Default));
        }
    }
}

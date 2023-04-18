using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public class DialogueViewModelBase : ViewModelBase, IWindowBaseViewModel
    {
        public ICommand CloseWindowCommand { get; }

        public Interaction<object?, object?> CloseWindow { get; } = new();

        public Interaction<object?, object?> MaximizeWindow { get; } = new();

        public Interaction<object?, object?> MinimizeWindow { get; } = new();

        public DialogueViewModelBase()
        {
            CloseWindowCommand = ReactiveCommand.CreateFromObservable(() => CloseWindow.Handle(null));
        }
    }
}

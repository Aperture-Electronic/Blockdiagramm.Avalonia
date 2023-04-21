using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels.Dialogues
{
    public class ExceptionErrorDialogViewModel : DialogueViewModelBase
    {
        private string message;
        private string working;
        private bool showWorking = false;

        public string Message
        {
            get => message;
            set => this.RaiseAndSetIfChanged(ref message, value);
        }

        public string Working
        {
            get => working;
            set
            {
                ShowWorking = !string.IsNullOrWhiteSpace(working);
                this.RaiseAndSetIfChanged(ref working, value);
            }
        }

        public bool ShowWorking
        {
            get => showWorking;
            private set => this.RaiseAndSetIfChanged(ref showWorking, value);   
        }

        public ICommand OKCommand { get; }

        public Interaction<Unit, Unit> OK { get; } = new();

        public ExceptionErrorDialogViewModel(string message, string working = "")
        {
            this.message = message;
            this.working = working;
            showWorking = !string.IsNullOrWhiteSpace(working);

            OKCommand = ReactiveCommand.CreateFromObservable(() => OK.Handle(Unit.Default));
        }
    }
}

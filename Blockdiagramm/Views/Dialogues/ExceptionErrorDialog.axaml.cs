using Avalonia.Controls;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using ReactiveUI;

namespace Blockdiagramm.Views.Dialogues
{
    public partial class ExceptionErrorDialog : DialogBase<ExceptionErrorDialogViewModel>
    {
        public ExceptionErrorDialog()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                RegisterBaseHandlers(d);

                d(ViewModel!.OK.RegisterHandler(OK));
            });
        }

        private void OK(InteractionContext<object?, object?> args) => CloseWindow(args);
    }
}

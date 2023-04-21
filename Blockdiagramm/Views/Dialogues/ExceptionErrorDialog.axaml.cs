using Avalonia.Controls;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using ReactiveUI;
using System.Reactive;

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

                d(ViewModel!.OK.RegisterHandler(CloseWindow));
            });
        }
    }
}

using System.Reactive;
using Avalonia.Controls;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using ReactiveUI;

namespace Blockdiagramm.Views.Dialogues
{
    public partial class AddComponentDialog : DialogBase<AddComponentDialogViewModel>
    {
        public AddComponentDialog()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                RegisterBaseHandlers(d);

                d(ViewModel!.ConfirmAddComponent.RegisterHandler(ConfirmAddComponent));
            });
        }

        private void ConfirmAddComponent(InteractionContext<Unit, bool> args)
        {
            if (DataContext is AddComponentDialogViewModel model)
            {
                model.Validate();

                if (model.ViewModelValid)
                {
                    Submit();
                    args.SetOutput(true);
                    return;
                }
            }

            args.SetOutput(false);
        }

        private void Submit() => Close(DataContext);
    }
}

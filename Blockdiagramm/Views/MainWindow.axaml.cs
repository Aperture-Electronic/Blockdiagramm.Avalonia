using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using Blockdiagramm.Models;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using Blockdiagramm.Views.Dialogues;
using ReactiveUI;
using System.Reactive;
using System.Threading.Tasks;

namespace Blockdiagramm.Views
{
    public partial class MainWindow : DiagrammeWindowBase<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                RegisterBaseHandlers(d);

                d(ViewModel!.NewProject.RegisterHandler(OpenNewProjectDialog));
            });
        }

        private async Task OpenNewProjectDialog(InteractionContext<object?, NewProjectDialogViewModel> args)
        {
            NewProjectDialog dialog = new();
            NewProjectDialogViewModel viewModel = new();
            dialog.DataContext = viewModel;
            var result = await dialog.ShowDialog<NewProjectDialogViewModel>(this);
            args.SetOutput(result);
        }   
    }
}
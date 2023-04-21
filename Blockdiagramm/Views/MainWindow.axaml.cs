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

                d(ViewModel!.ShowException.RegisterHandler(ShowException));
                d(ViewModel!.ShowProcessingWait.RegisterHandler(ShowProcessingWait));

                d(ViewModel!.NewProject.RegisterHandler(OpenNewProjectDialog));

                d(ViewModel!.AddSourceFile.RegisterHandler(OpenAddSourceFileDialog));
            });
        }

        private async Task ShowException(InteractionContext<ExceptionErrorDialogViewModel, Unit> args)
        {
            var model = args.Input;
            ExceptionErrorDialog dialog = new()
            {
                DataContext = model
            };
            await dialog.ShowDialog(this);
            args.SetOutput(Unit.Default);
        }

        private async Task OpenNewProjectDialog(InteractionContext<Unit, NewProjectDialogViewModel> args)
        {
            NewProjectDialog dialog = new();
            NewProjectDialogViewModel viewModel = new();
            dialog.DataContext = viewModel;
            var result = await dialog.ShowDialog<NewProjectDialogViewModel>(this);
            args.SetOutput(result);
        }   

        private async Task OpenAddSourceFileDialog(InteractionContext<Unit, AddSourceFileDialogViewModel> args)
        {
            AddSourceFileDialog dialog = new();
            AddSourceFileDialogViewModel viewModel = new();
            dialog.DataContext = viewModel;
            var result = await dialog.ShowDialog<AddSourceFileDialogViewModel>(this);
            args.SetOutput(result);
        }

        private void ShowProcessingWait(InteractionContext<ProcessingWaitDialogViewModel, ProcessingWaitDialog> args)
        {
            ProcessingWaitDialog dialog = new()
            {
                DataContext = args.Input
            };

            dialog.ShowDialog(this);
            args.SetOutput(dialog);
        }
    }
}
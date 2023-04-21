using Avalonia;
using Avalonia.Controls;
using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels.Dialogues;
using Blockdiagramm.Views.Dialogues;
using DynamicData;
using ReactiveUI;
using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public partial class MainWindowViewModel : ReactiveObject, IWindowBaseViewModel
    {
        public ICommand CloseWindowCommand { get; }
        public ICommand MaximizeWindowCommand { get; }
        public ICommand MinimizeWindowCommand { get; }

        public TitleBarViewModel TitleBarModel { get; }
        public RibbonPanelViewModel RibbonPanelViewModel { get; }

        public Interaction<Unit, Unit> CloseWindow { get; } = new();
        public Interaction<Unit, Unit> MaximizeWindow { get; } = new();
        public Interaction<Unit, Unit> MinimizeWindow { get; } = new();

        public Interaction<ExceptionErrorDialogViewModel, Unit> ShowException { get; } = new();

        public Interaction<ProcessingWaitDialogViewModel, ProcessingWaitDialog> ShowProcessingWait { get; } = new();

        public MainWindowViewModel()
        {
            #region Common view model
            TitleBarModel = new(this, "beta", GlobalStatic.Project);
            RibbonPanelViewModel = new(this, GlobalStatic.Project);
            #endregion

            #region Source view model
            SourceFileListViewModel = new(GlobalStatic.Project.SourceFiles.Connect(), (file, filterText) => file.ShortName.Contains(filterText));

            #endregion

            #region Common commands
            CloseWindowCommand = ReactiveCommand.CreateFromObservable(() => CloseWindow.Handle(Unit.Default));
            MaximizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MaximizeWindow.Handle(Unit.Default));
            MinimizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MinimizeWindow.Handle(Unit.Default));
            #endregion

            #region Side panel commands
            SelectSidePanelCommand = ReactiveCommand.Create<string>(indexString => SelectedSidePanelIndex = int.Parse(indexString));
            #endregion

            #region Project commands
            NewProjectCommand = ReactiveCommand.CreateFromTask(NewProjectTask);
            #endregion

            #region Source commands
            AddSourceFileCommand = ReactiveCommand.CreateFromTask(AddSourceTask);
            #endregion

            #region Elaborate commands
            ElaborateAllCommand = ReactiveCommand.CreateFromTask(ElaborateAllTask);
            #endregion
        }
    }
}
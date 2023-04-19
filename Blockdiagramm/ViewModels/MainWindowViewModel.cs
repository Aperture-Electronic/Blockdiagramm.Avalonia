using Avalonia;
using Avalonia.Controls;
using Blockdiagramm.Logic;
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

        public Interaction<object?, object?> CloseWindow { get; } = new();
        public Interaction<object?, object?> MaximizeWindow { get; } = new();
        public Interaction<object?, object?> MinimizeWindow { get; } = new();

        public MainWindowViewModel()
        {
            TitleBarModel = new(this, "beta", GlobalStatic.Project);
            RibbonPanelViewModel = new(this, GlobalStatic.Project);

            CloseWindowCommand = ReactiveCommand.CreateFromObservable(() => CloseWindow.Handle(null));
            MaximizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MaximizeWindow.Handle(null));
            MinimizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MinimizeWindow.Handle(null));

            #region Project commands
            NewProjectCommand = ReactiveCommand.CreateFromTask(NewProjectTask);
            #endregion

            #region Source commands
            AddSourceFileCommand = ReactiveCommand.CreateFromTask(AddSourceTask);
            #endregion
        }
    }
}
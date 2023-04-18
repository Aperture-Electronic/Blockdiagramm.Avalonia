using Avalonia;
using Avalonia.Controls;
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

        public Interaction<object?, object?> CloseWindow { get; } = new();
        public Interaction<object?, object?> MaximizeWindow { get; } = new();
        public Interaction<object?, object?> MinimizeWindow { get; } = new();

        public MainWindowViewModel()
        {
            CloseWindowCommand = ReactiveCommand.CreateFromObservable(() => CloseWindow.Handle(null));
            MaximizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MaximizeWindow.Handle(null));
            MinimizeWindowCommand = ReactiveCommand.CreateFromObservable(() => MinimizeWindow.Handle(null));

            #region Project commands
            NewProjectCommand = ReactiveCommand.CreateFromTask(async () => await NewProject.Handle(null));
            #endregion
        }
    }
}
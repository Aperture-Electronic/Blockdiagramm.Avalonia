using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.ReactiveUI;
using Blockdiagramm.ViewModels;
using ReactiveUI;
using System.Reactive;

namespace Blockdiagramm.Views
{
    public partial class MainWindow : ReactiveWindow<MainWindowViewModel>
    {
        public MainWindow()
        {
            InitializeComponent();
            this.WhenActivated(d =>
            {
                d(ViewModel!.CloseWindow.RegisterHandler(CloseWindow));
                d(ViewModel!.MaximizeWindow.RegisterHandler(MaximizeWindow));
                d(ViewModel!.MinimizeWindow.RegisterHandler(MinimizeWindow));
            });
        }

        private void CloseWindow(InteractionContext<object?, object?> args)
        {
            Close();
            args.SetOutput(null);
        }

        private void MaximizeWindow(InteractionContext<object?, object?> args)
        {
            WindowState = WindowState == WindowState.Maximized ?
                WindowState.Normal : WindowState.Maximized;
            args.SetOutput(null);
        }

        private void MinimizeWindow(InteractionContext<object?, object?> args)
        {
            WindowState = WindowState.Minimized;
            args.SetOutput(null);
        }


        private void BeginDrag(object sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
            e.Handled = true;
        }
    }
}
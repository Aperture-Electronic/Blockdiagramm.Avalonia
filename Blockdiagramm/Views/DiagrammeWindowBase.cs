using Avalonia.Input;
using Avalonia.ReactiveUI;
using ReactiveUI;
using System;
using Avalonia.Controls;
using Blockdiagramm.ViewModels;
using Blockdiagramm.Views.Dialogues;
using System.Reactive;
using System.Threading.Tasks;

namespace Blockdiagramm.Views
{
    public abstract class DiagrammeWindowBase<T> : ReactiveWindow<T> where T : class, IWindowBaseViewModel
    {
        protected void RegisterBaseHandlers(Action<IDisposable> d)
        {
            d(ViewModel!.CloseWindow.RegisterHandler(CloseWindow));
            d(ViewModel!.MaximizeWindow.RegisterHandler(MaximizeWindow));
            d(ViewModel!.MinimizeWindow.RegisterHandler(MinimizeWindow));
        }

        protected void CloseWindow(InteractionContext<Unit, Unit> args)
        {
            Close();
            args.SetOutput(Unit.Default);
        }

        protected void MaximizeWindow(InteractionContext<Unit, Unit> args)
        {
            WindowState = WindowState == WindowState.Maximized ?
                WindowState.Normal : WindowState.Maximized;
            args.SetOutput(Unit.Default);
        }

        protected void MinimizeWindow(InteractionContext<Unit, Unit> args)
        {
            WindowState = WindowState.Minimized;
            args.SetOutput(Unit.Default);
        }


        protected void BeginDrag(object sender, PointerPressedEventArgs e)
        {
            BeginMoveDrag(e);
            e.Handled = true;
        }
    }
}

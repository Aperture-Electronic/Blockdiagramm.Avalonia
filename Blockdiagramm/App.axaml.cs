using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using Blockdiagramm.ViewModels;
using Blockdiagramm.Views;

namespace Blockdiagramm
{
    public partial class App : Application
    {
        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow();
                desktop.MainWindow.DataContext = new MainWindowViewModel();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
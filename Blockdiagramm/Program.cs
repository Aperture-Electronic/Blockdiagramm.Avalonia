using Avalonia;
using Avalonia.ReactiveUI;
using Avalonia.Svg.Skia;
using Projektanker.Icons.Avalonia;
using Projektanker.Icons.Avalonia.MaterialDesign;
using System;

namespace Blockdiagramm
{
    internal class Program
    {
        [STAThread]
        public static void Main(string[] args) => BuildAvaloniaApp()
            .StartWithClassicDesktopLifetime(args);

        public static AppBuilder BuildAvaloniaApp()
        {
            GC.KeepAlive(typeof(SvgImageExtension).Assembly);
            GC.KeepAlive(typeof(Avalonia.Svg.Skia.Svg).Assembly);

            return AppBuilder.Configure<App>()
                .UsePlatformDetect()
                .LogToTrace()
                .UseReactiveUI()
                .WithIcons(container => container.Register<MaterialDesignIconProvider>());
        }
    }
}
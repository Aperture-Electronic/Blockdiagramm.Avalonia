using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Ribbon
{
    public abstract class RibbonButtonControl : UserControl
    {
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<RibbonButtonControl, string>(nameof(Title), "Title",
                defaultBindingMode: BindingMode.OneWay);


        public static readonly StyledProperty<string> IconProperty =
            AvaloniaProperty.Register<RibbonButtonControl, string>(nameof(Icon), "mdi-flask-empty-outline",
                defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<string> TooltipTitleProperty = 
            AvaloniaProperty.Register<RibbonButtonControl, string>(nameof(TooltipTitle), "Tooltip Title",
                               defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<object> TooltipContentProperty =
            AvaloniaProperty.Register<RibbonButtonControl, object>(nameof(TooltipContent),
                defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<IReactiveCommand> CommandProperty =
            AvaloniaProperty.Register<RibbonButtonControl, IReactiveCommand>(nameof(Command));

        public string Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public string Icon
        {
            get => GetValue(IconProperty);
            set => SetValue(IconProperty, value);
        }

        public string TooltipTitle
        {
            get => GetValue(TooltipTitleProperty);
            set => SetValue(TooltipTitleProperty, value);
        }

        public object TooltipContent
        {
            get => GetValue(TooltipContentProperty);
            set => SetValue(TooltipContentProperty, value);
        }

        public IReactiveCommand Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }
    }
}

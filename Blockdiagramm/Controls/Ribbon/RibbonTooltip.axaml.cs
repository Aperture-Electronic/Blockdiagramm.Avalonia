using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using JetBrains.Annotations;

namespace Blockdiagramm.Controls.Ribbon
{
    public partial class RibbonTooltip : UserControl
    {
        public static readonly StyledProperty<string> TooltipTitleProperty =
            AvaloniaProperty.Register<RibbonCard, string>(nameof(TooltipTitle));

        public static readonly StyledProperty<object> TooltipContentProperty =
            AvaloniaProperty.Register<RibbonCard, object>(nameof(TooltipContent));

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

        public RibbonTooltip()
        {
            InitializeComponent();
        }
    }
}

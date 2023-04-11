using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Blockdiagramm.Models.Ribbon;

namespace Blockdiagramm.Controls.Ribbon
{
    public partial class RibbonTooltip : UserControl
    {
        public static readonly StyledProperty<RibbonTooltipModel> TooltipProperty =
            AvaloniaProperty.Register<RibbonCard, RibbonTooltipModel>(nameof(Tooltip),
#if DEBUG
                defaultValue: new("Tooltip title", "Tooltip content"),
#endif
                defaultBindingMode: BindingMode.OneWay);

        public RibbonTooltipModel Tooltip
        {
            get => GetValue(TooltipProperty);
            set => SetValue(TooltipProperty, value);
        }

        public RibbonTooltip()
        {
            InitializeComponent();
        }
    }
}

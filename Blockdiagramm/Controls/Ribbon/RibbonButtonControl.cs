using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Blockdiagramm.Models.Ribbon;
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
            AvaloniaProperty.Register<BigButton, string>(nameof(Title), "Title",
                defaultBindingMode: BindingMode.OneWay);


        public static readonly StyledProperty<string> IconProperty =
            AvaloniaProperty.Register<RibbonCard, string>(nameof(Icon), "mdi-flask-empty-outline",
                defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<RibbonTooltipModel> TooltipProperty =
            AvaloniaProperty.Register<RibbonCard, RibbonTooltipModel>(nameof(Tooltip),
                defaultBindingMode: BindingMode.OneWay);

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

        public RibbonTooltipModel Tooltip
        {
            get => GetValue(TooltipProperty);
            set => SetValue(TooltipProperty, value);
        }
    }
}

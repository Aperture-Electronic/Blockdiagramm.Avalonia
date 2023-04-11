using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;
using Avalonia.Interactivity;
using System;

namespace Blockdiagramm.Controls.Ribbon
{
    public partial class RibbonMenuItem : RibbonButtonControl
    {
        public static readonly StyledProperty<string> ShortcutProperty =
            AvaloniaProperty.Register<RibbonMenuItem, string>(nameof(Shortcut), "",
                defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<bool> IsFirstProperty =
            AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsFirst),
                defaultValue: true,
                defaultBindingMode: BindingMode.OneWay);

        public static readonly StyledProperty<bool> IsLastProperty =
            AvaloniaProperty.Register<RibbonMenuItem, bool>(nameof(IsLast),
                defaultValue: false,
                defaultBindingMode: BindingMode.OneWay);

        public static readonly RoutedEvent ClickEvent =
            RoutedEvent.Register<RibbonMenuItem, RoutedEventArgs>(nameof(Click), RoutingStrategies.Bubble);

        public string Shortcut
        {
            get => GetValue(ShortcutProperty);
            set => SetValue(ShortcutProperty, value);
        }

        public bool IsFirst
        {
            get => GetValue(IsFirstProperty);
            set => SetValue(IsFirstProperty, value);
        }

        public bool IsLast
        {
            get => GetValue(IsLastProperty);
            set => SetValue(IsLastProperty, value);
        }

        public event EventHandler<RoutedEventArgs> Click
        {
            add => AddHandler(ClickEvent, value);
            remove => RemoveHandler(ClickEvent, value);
        }

        public RibbonMenuItem()
        {
            InitializeComponent();
        }

        private void OnMenuButtonClick(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = ClickEvent;
            RaiseEvent(e);
        }
    }
}

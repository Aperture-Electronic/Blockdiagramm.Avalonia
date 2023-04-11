using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Blockdiagramm.Controls
{
    public partial class TitleBar : UserControl
    {
        public readonly static RoutedEvent<PointerPressedEventArgs> BeginDragEvent =
            RoutedEvent.Register<TitleBar, PointerPressedEventArgs>(nameof(BeginDrag), RoutingStrategies.Bubble);

        public event EventHandler<PointerPressedEventArgs> BeginDrag
        {
            add => AddHandler(BeginDragEvent, value);
            remove => RemoveHandler(BeginDragEvent, value);
        }

        public TitleBar()
        {
            InitializeComponent();
        }

        private void OnTitleBarBeginDrag(object sender, PointerPressedEventArgs e)
        {
            e.RoutedEvent = BeginDragEvent;
            RaiseEvent(e);
        }
    }
}

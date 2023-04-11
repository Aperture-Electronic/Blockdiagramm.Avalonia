using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;
using System.Drawing;

namespace Blockdiagramm.Controls.Diagram.Component
{
    public partial class ComponentPort : UserControl
    {
        public readonly static RoutedEvent<ComponentPortPressedEventArgs> PortPressedEvent =
            RoutedEvent.Register<ComponentPort, ComponentPortPressedEventArgs>(nameof(PortPressed), RoutingStrategies.Bubble);

        public event EventHandler<ComponentPortPressedEventArgs> PortPressed
        {
            add => AddHandler(PortPressedEvent, value);
            remove => RemoveHandler(PortPressedEvent, value);
        }

        public ComponentPort()
        {
            InitializeComponent();
        }

        private void OnPortStackPointerEnter(object sender, PointerEventArgs e)
        {
            stack.Classes.Add("Selected");
        }

        private void OnPortStackPointerLeave(object sender, PointerEventArgs e)
        {
            stack.Classes.Remove("Selected");
        }

        private void OnPortStackPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (DataContext is ComponentPortModel portModel)
            {
                ComponentPortPressedEventArgs portEventArgs = new(PortPressedEvent, portModel);
                RaiseEvent(portEventArgs);
            }

            // Stop popup the event to the component
            e.Handled = true;
        }
    }
}

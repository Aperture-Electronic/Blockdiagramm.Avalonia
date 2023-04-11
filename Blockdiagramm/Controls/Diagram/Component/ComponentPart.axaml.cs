using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Blockdiagramm.Extensions;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;

namespace Blockdiagramm.Controls.Diagram.Component
{
    public partial class ComponentPart : UserControl
    {
        private bool isDragging = false;
        private Point mousePosition;
        private Point previousPosition = (0.0, 0.0).ToPoint();

        public readonly static StyledProperty<bool> IsSelectedProperty = 
            AvaloniaProperty.Register<ComponentPart, bool>(nameof(IsSelected), false);

        public readonly static RoutedEvent DoubleTappedInstanceNameEvent =
            RoutedEvent.Register<ComponentPart, RoutedEventArgs>(nameof(DoubleTappedInstanceName), RoutingStrategies.Bubble);

        public readonly static RoutedEvent<ComponentPortPressedEventArgs> PortPressedEvent =
            RoutedEvent.Register<ComponentPart, ComponentPortPressedEventArgs>(nameof(PortPressed), RoutingStrategies.Bubble);

        public bool IsSelected
        {
            get => GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public event EventHandler<RoutedEventArgs> DoubleTappedInstanceName
        {
            add => AddHandler(DoubleTappedInstanceNameEvent, value);
            remove => RemoveHandler(DoubleTappedInstanceNameEvent, value);
        }

        public event EventHandler<ComponentPortPressedEventArgs> PortPressed
        {
            add => AddHandler(PortPressedEvent, value);
            remove => AddHandler(PortPressedEvent, value);
        }

        public ComponentPart()
        {
            InitializeComponent();
        }

        private void OnComponentBodyPointerPressed(object sender, PointerPressedEventArgs e)
        {
            isDragging = true;
            IsSelected = true;
            mousePosition = e.GetPosition(Parent);
        }

        private void OnComponentBodyPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            isDragging = false;
            IsSelected = false;

            if (RenderTransform is TranslateTransform transform)
            {
                previousPosition = (transform.X, transform.Y).ToPoint();
            }
        }

        private void OnComponentBodyPointerMoved(object sender, PointerEventArgs e)
        {
            if (isDragging)
            {
                Point currentPosition = e.GetPosition(Parent);
                RenderTransform ??= new TranslateTransform();

                if (RenderTransform is TranslateTransform transform)
                {
                    Point position = currentPosition - mousePosition + previousPosition;
                    Point positionSnap = GridSnap.SnapToRectGrid(position, 10);

                    transform.X = positionSnap.X;
                    transform.Y = positionSnap.Y;
                }
            }
        }
    
        private void OnInstanceNameDoubleTapped(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = DoubleTappedInstanceNameEvent;
            RaiseEvent(e);
        }

        private void OnPortPressed(object sender, ComponentPortPressedEventArgs e)
        {
            if (DataContext is ComponentPartModel partModel)
            {
                Point position = partModel.GetPortPosition(e.PortModel, Bounds);
                e.OnComponentPosition = position;
                e.PartModel = partModel;
                e.RoutedEvent = PortPressedEvent;
                RaiseEvent(e);
            }      
        }
    }
}

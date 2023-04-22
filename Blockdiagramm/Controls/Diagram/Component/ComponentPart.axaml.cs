using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Media;
using Blockdiagramm.Extensions;
using Blockdiagramm.Models;
using Blockdiagramm.Renderer.Wiring.Router;
using Blockdiagramm.ViewModels.Diagram;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;

namespace Blockdiagramm.Controls.Diagram.Component
{
    public partial class ComponentPart : UserControl, IRectObstacle, IPart<ComponentPartInstanceModel, Models.ComponentPortModel>, IDiagramItem
    {
        private bool isDragging = false;
        private Point mousePosition;
        private Point previousPosition = (0.0, 0.0).ToPoint();

        public ComponentPartInstanceModel? Model => DataContext as ComponentPartInstanceModel;

        public readonly static StyledProperty<bool> IsSelectedProperty = 
            AvaloniaProperty.Register<ComponentPart, bool>(nameof(IsSelected), false);

        public readonly static RoutedEvent DoubleTappedInstanceNameEvent =
            RoutedEvent.Register<ComponentPart, RoutedEventArgs>(nameof(DoubleTappedInstanceName), RoutingStrategies.Bubble);

        public readonly static RoutedEvent<ComponentPortPressedEventArgs> PortPressedEvent =
            RoutedEvent.Register<ComponentPart, ComponentPortPressedEventArgs>(nameof(PortPressed), RoutingStrategies.Bubble);

        public readonly static RoutedEvent<ComponentMovedEventArgs> ComponentMovedEvent =
            RoutedEvent.Register<ComponentPart, ComponentMovedEventArgs>(nameof(ComponentMoved), RoutingStrategies.Bubble);

        public DiagramItemType DiagramType => DiagramItemType.ComponentPart;

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
            remove => RemoveHandler(PortPressedEvent, value);
        }

        public event EventHandler<ComponentMovedEventArgs> ComponentMoved
        {
            add => AddHandler(ComponentMovedEvent, value);
            remove => RemoveHandler(ComponentMovedEvent, value);
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
                Point currentPosition = (transform.X, transform.Y).ToPoint();

                if (currentPosition != previousPosition)
                {
                    // Create the arguments for event
                    ComponentMovedEventArgs args = new(previousPosition, currentPosition);
                    args.RoutedEvent = ComponentMovedEvent;

                    // Update previous positon
                    previousPosition = currentPosition;

                    // Raise the event
                    RaiseEvent(args);
                }
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
            if (DataContext is ComponentPartInstanceModel partModel)
            {
                e.PartModel = partModel;
                e.RoutedEvent = PortPressedEvent;
                RaiseEvent(e);
            }      
        }

        public Point GetPortPosition(IPort<IPortModel> port)
        {
            if (Model == null)
            {
                throw new Exception("The part has not a valid data context of part model");
            }

            if (port.Model is not Models.ComponentPortModel model)
            {
                throw new Exception("The port has not a valid data context of port model");
            }

            Point transformPoint = (RenderTransform is TranslateTransform partTransform) ?
                (partTransform.X, partTransform.Y).ToPoint() : new();
            Point canvasPoint = (Canvas.GetLeft(this).NaNAsZero(), Canvas.GetTop(this).NaNAsZero()).ToPoint();
            Point portRelativePosition = Model.GetPortPosition(model, Bounds);
            
            return transformPoint + canvasPoint + portRelativePosition; 
        }

        public PortDirection GetPortDirection(IPort<IPortModel> port)
        {
            if (port.Model is not Models.ComponentPortModel model)
            {
                throw new Exception("The port has not a valid data context of port model");
            }

            return model.Direction;
        }

        public bool IsObstacleValid { get; set; } = true;

        /// <summary>
        /// The bound box of component for hit test
        /// </summary>
        public Rect BoundBox
        {
            get
            {
                if (RenderTransform is TranslateTransform transfrom)
                {
                    return new Rect(transfrom.X, transfrom.Y, Bounds.Width, Bounds.Height);
                }

                return new Rect(0, 0, Bounds.Width, Bounds.Height);
            }
        }
    }
}

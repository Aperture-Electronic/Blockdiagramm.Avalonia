using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Blockdiagramm.Views;
using System;
using System.Windows.Input;

namespace Blockdiagramm.Controls
{
    public partial class DialogueFrame : UserControl
    {
        public static readonly StyledProperty<string> TitleProperty
            = AvaloniaProperty.Register<DialogueFrame, string>(nameof(Title), "Title");

        public static readonly StyledProperty<string> IconProperty
            = AvaloniaProperty.Register<DialogueFrame, string>(nameof(Icon), "mdi-flask-empty-outline");

        public static readonly StyledProperty<object?> DialogContentProperty
            = AvaloniaProperty.Register<DialogueFrame, object?>(nameof(DialogContent));

        public static readonly StyledProperty<string> CancelButtonTextProperty
            = AvaloniaProperty.Register<DialogueFrame, string>(nameof(CancelButtonText), "Cancel");

        public static readonly StyledProperty<string> ConfirmButtonTextProperty
            = AvaloniaProperty.Register<DialogueFrame, string>(nameof(ConfirmButtonText), "OK");

        public static readonly StyledProperty<ICommand> ConfirmCommandProperty
            = AvaloniaProperty.Register<DialogueFrame, ICommand>(nameof(ConfirmCommand));

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

        public object? DialogContent
        {
            get => GetValue(DialogContentProperty);
            set => SetValue(DialogContentProperty, value);
        }

        public string CancelButtonText
        {
            get => GetValue(CancelButtonTextProperty);
            set => SetValue(CancelButtonTextProperty, value);
        }

        public string ConfirmButtonText
        {
            get => GetValue(ConfirmButtonTextProperty);
            set => SetValue(ConfirmButtonTextProperty, value);
        }

        public ICommand ConfirmCommand
        {
            get => GetValue(ConfirmCommandProperty);
            set => SetValue(ConfirmCommandProperty, value);
        }

        public readonly static RoutedEvent<PointerPressedEventArgs> BeginDragEvent =
            RoutedEvent.Register<TitleBar, PointerPressedEventArgs>(nameof(BeginDrag), RoutingStrategies.Bubble);

        public event EventHandler<PointerPressedEventArgs> BeginDrag
        {
            add => AddHandler(BeginDragEvent, value);
            remove => RemoveHandler(BeginDragEvent, value);
        }

        public DialogueFrame()
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

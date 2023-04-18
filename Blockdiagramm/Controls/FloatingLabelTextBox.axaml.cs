using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Blockdiagramm.Controls
{
    public partial class FloatingLabelTextBox : UserControl
    {
        public readonly static StyledProperty<int> LengthLimitProperty =
            AvaloniaProperty.Register<FloatingLabelTextBox, int>(nameof(LengthLimit), 10);

        public readonly static StyledProperty<string> TipTextProperty =
            AvaloniaProperty.Register<FloatingLabelTextBox, string>(nameof(TipText));

        public readonly static StyledProperty<string> InvalidTipTextProperty = 
            AvaloniaProperty.Register<FloatingLabelTextBox, string>(nameof(InvalidTipText));

        public readonly static StyledProperty<string> PlaceholderProperty =
            AvaloniaProperty.Register<FloatingLabelTextBox, string>(nameof(Placeholder));

        public readonly static StyledProperty<bool> IsInvalidProperty  = 
            AvaloniaProperty.Register<FloatingLabelTextBox, bool>(nameof(IsInvalid));

        public readonly static StyledProperty<string> TextProperty =
            AvaloniaProperty.Register<FloatingLabelTextBox, string>(nameof(Text), defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);

        public readonly static RoutedEvent<RoutedEventArgs> TextBoxLostFocusEvent =
            RoutedEvent.Register<FloatingLabelTextBox, RoutedEventArgs>(nameof(LostFocus), RoutingStrategies.Bubble);

        public int LengthLimit
        {
            get => GetValue(LengthLimitProperty);
            set => SetValue(LengthLimitProperty, value);
        }

        public string TipText
        {
            get => GetValue(TipTextProperty);
            set => SetValue(TipTextProperty, value);
        }

        public string InvalidTipText
        {
            get => GetValue(InvalidTipTextProperty);
            set => SetValue(InvalidTipTextProperty, value);
        }   

        public string Placeholder
        {
            get => GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }

        public bool IsInvalid
        {
            get => GetValue(IsInvalidProperty);
            set => SetValue(IsInvalidProperty, value);
        }

        public event EventHandler<RoutedEventArgs> TextBoxLostFocus
        {
            add => AddHandler(TextBoxLostFocusEvent, value);
            remove => RemoveHandler(TextBoxLostFocusEvent, value);
        }

        public FloatingLabelTextBox()
        {
            InitializeComponent();       
        }

        private void OnClearButtonClick(object sender, RoutedEventArgs e) => textbox.Text = string.Empty;

        private void OnTextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            e.RoutedEvent = TextBoxLostFocusEvent;
            RaiseEvent(e);
        }
    }
}

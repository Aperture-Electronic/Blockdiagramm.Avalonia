using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using System;

namespace Blockdiagramm.Controls
{
    public partial class FloatingLabelComboBox : UserControl
    {
        public readonly static StyledProperty<string> PlaceholderProperty =
            AvaloniaProperty.Register<FloatingLabelTextBox, string>(nameof(Placeholder));

        public readonly static StyledProperty<AvaloniaList<object>> SelectableItemsProperty = 
            AvaloniaProperty.Register<FloatingLabelComboBox, AvaloniaList<object>>(nameof(SelectableItems));

        public readonly static StyledProperty<bool> IsOpenedProperty = 
            AvaloniaProperty.Register<FloatingLabelComboBox, bool>(nameof(IsOpened));

        public readonly static StyledProperty<string> TextProperty = 
            AvaloniaProperty.Register<FloatingLabelComboBox, string>(nameof(Text));

        public readonly static DirectProperty<FloatingLabelComboBox, int> SelectedIndexProperty =
            AvaloniaProperty.RegisterDirect<FloatingLabelComboBox, int>(nameof(SelectedIndex), 
                (e) => e.SelectedIndex, (e, v) => e.SelectedIndex = v);

        public readonly static DirectProperty<FloatingLabelComboBox, object?> SelectedItemProperty = 
            AvaloniaProperty.RegisterDirect<FloatingLabelComboBox, object?>(nameof(SelectedItem),
                (e) => e.SelectedItem);

        public readonly static RoutedEvent<ComboBoxSelectedEventArgs> ComboSelectedEvent =
            RoutedEvent.Register<FloatingLabelComboBox, ComboBoxSelectedEventArgs>(nameof(ComboSelected), RoutingStrategies.Bubble);

        private object? selectedItem = null;
        private int selectedIndex = -1;

        public AvaloniaList<object> SelectableItems
        {
            get => GetValue(SelectableItemsProperty);
            set => SetValue(SelectableItemsProperty, value);
        }

        public string Placeholder
        {
            get => GetValue(PlaceholderProperty);
            set => SetValue(PlaceholderProperty, value);
        }

        public bool IsOpened
        {
            get => GetValue(IsOpenedProperty);
            set => SetValue(IsOpenedProperty, value);
        }

        public string Text
        {
            get => GetValue(TextProperty); 
            set => SetValue(TextProperty, value);
        }

        public object? SelectedItem
        {
            get => selectedItem;
            private set => SetAndRaise(SelectedItemProperty, ref selectedItem, value);
        }

        public int SelectedIndex
        {
            get => selectedIndex;
            set
            {
                SelectItem(value);
                SetAndRaise(SelectedIndexProperty, ref selectedIndex, value);
            }
        }

        public event EventHandler<ComboBoxSelectedEventArgs> ComboSelected
        {
            add => AddHandler(ComboSelectedEvent, value);
            remove => RemoveHandler(ComboSelectedEvent, value); 
        }

        private void OnItemPointerPressed(object sender, PointerPressedEventArgs e)
        {
            if (sender is ComboBoxItem item)
            {
                // Try to get the index in the list
                int index = SelectableItems.IndexOf(item.Content);
                if (index < 0)
                {
                    return;
                }

                // Close the item list
                IsOpened = false;

                // Select the item
                SelectedIndex = index;

                // Raise the event
                var eventArgs = new ComboBoxSelectedEventArgs(SelectedItem, index)
                {
                    RoutedEvent = ComboSelectedEvent
                };

                RaiseEvent(eventArgs);
            }
        }

        private void OnMaskPointerPressed(object sender, PointerPressedEventArgs e)
        {
            IsOpened = !IsOpened;
        }

        private void OnMaskLostFocus(object sender, RoutedEventArgs e)
        {
            IsOpened = false;
        }

        private void SelectItem(int index)
        {
            if (index >= SelectableItems.Count)
            {
                return;
            }

            SelectedItem = SelectableItems[index];
            Text = SelectedItem.ToString() ?? "";
        }

        public FloatingLabelComboBox()
        {
            InitializeComponent();
        }
    }
}

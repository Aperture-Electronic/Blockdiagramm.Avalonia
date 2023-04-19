using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls
{
    public class ComboBoxSelectedEventArgs : RoutedEventArgs
    {
        public object? SelectedItem { get; }

        public int Index { get; }

        public ComboBoxSelectedEventArgs(object? selectedItem, int index)
        {
            SelectedItem = selectedItem;
            Index = index;
        }
    }
}

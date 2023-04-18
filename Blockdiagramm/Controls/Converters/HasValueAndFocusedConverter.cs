using Avalonia.Controls;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    internal class HasValueAndFocusedConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count != 2) return false;
            if (values[0] is not string text || values[1] is not bool isFocused)
            {
                return false;
            }

            return (!string.IsNullOrEmpty(text)) || isFocused;
        }
    }
}

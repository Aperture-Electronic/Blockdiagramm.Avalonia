using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class InvertConverter : IValueConverter
    {
        private static object? Invert(object? value)
        {
            if (value is bool boolValue)
            {
                return !boolValue;
            }

            return null;
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture) => Invert(value);

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => Invert(value);
    }
}

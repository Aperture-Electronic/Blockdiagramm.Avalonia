using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;

namespace Blockdiagramm.Controls.Diagram.Converters
{
    public class XYMarginConverter : IMultiValueConverter, IValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((values.Count == 1) && (values[0] is double uniform))
            {
                return new Thickness(uniform);
            }

            if ((values.Count == 2) && (values[0] is double horizontal) && (values[1] is double vertical))
            {
                if (parameter is string paramString)
                {
                    string[] offsetString = paramString.Split(',');
                    parameter = (double.Parse(offsetString[0]), double.Parse(offsetString[1]));
                }

                if (parameter is (double xOffset, double yOffset))
                {
                    return new Thickness(horizontal + xOffset, vertical + yOffset);
                }

                return new Thickness(horizontal, vertical);
            }

            return new Thickness(0);
        }

        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
            => Convert(new List<object?>() { value }, targetType, parameter, culture);

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

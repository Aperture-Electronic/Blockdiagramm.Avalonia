using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class ValueEqualConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int numValue)
            {
                int paramValue = parameter switch
                {
                    string pStringValue => int.Parse(pStringValue),
                    int pIntValue => pIntValue,
                    _ => throw new Exception($"The parameter of {nameof(ValueEqualConverter)} is invalid")
                };

                return numValue == paramValue;
            }

            return false;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class ValueAndLimitIndicateConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((values[1] is not int limit))
            {
                return "";
            }

            if (limit <= 0)
            {
                return "";
            }

            if ((values[0] is not int value))
            {
                return $"0/{limit}";
            }

            return $"{value}/{limit}";
        }
    }
}

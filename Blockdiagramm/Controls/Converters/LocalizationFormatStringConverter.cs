using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class LocalizationFormatStringConverter : IMultiValueConverter
    {
        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count < 1) return null;

            if (values[0] is string formatString)
            {
                if (values.Count > 1)
                {
                    var arguments = values.Skip(1).ToArray();
                    return string.Format(formatString, arguments);
                }

                return formatString;
            }

            return values[0];
        }
    }
}

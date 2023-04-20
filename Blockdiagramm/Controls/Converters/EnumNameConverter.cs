using Avalonia.Data.Converters;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class EnumNameConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is Enum enumValue)
            {
                return enumValue.ToString().SplitByCapital();
            }

            return "";
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((value is string str) && targetType.IsEnum)
            {
                if (Enum.TryParse(targetType, str, ignoreCase: true, out var enumValue))
                {
                    return enumValue;
                }
                else
                {
                    return null;
                }
            }

            return null;
        }
    }
}

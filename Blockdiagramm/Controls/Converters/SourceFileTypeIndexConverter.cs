using Avalonia.Data.Converters;
using Blockdiagramm.Logic;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Converters
{
    public class SourceFileTypeIndexConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is string stringValue)
            {
                string[] names = Enum.GetNames(typeof(SourceFileType));
                if (names.Contains(stringValue))
                {
                    return Array.IndexOf(names, stringValue);
                }
            }

            if (value is SourceFileType fileTypeValue)
            {
                Array values = Enum.GetValues(typeof(SourceFileType));
                return Array.IndexOf(values, fileTypeValue);
            }

            return 0;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is int index)
            {
                Array values = Enum.GetValues(typeof(SourceFileType));
                return values.GetValue(index);
            }

            return SourceFileType.Auto;
        }
    }
}

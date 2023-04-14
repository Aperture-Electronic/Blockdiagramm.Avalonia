using Avalonia.Data.Converters;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Converters
{
    public class PortDirectionToZIndexConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is PortDirection direction)
            {
                return direction == PortDirection.Master ? -1 : 1;
            }

            return -1;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((value is int zIndex) && (targetType == typeof(PortDirection)))
            {
                return zIndex >= 0 ? PortDirection.Master : PortDirection.Slave;
            }

            return null;
        }
    }
}

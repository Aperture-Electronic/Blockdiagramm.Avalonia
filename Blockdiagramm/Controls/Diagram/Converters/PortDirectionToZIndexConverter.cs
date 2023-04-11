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
            if (value is ComponentPortDirection direction)
            {
                return direction == ComponentPortDirection.Master ? -1 : 1;
            }

            return -1;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if ((value is int zIndex) && (targetType == typeof(ComponentPortDirection)))
            {
                return zIndex >= 0 ? ComponentPortDirection.Master : ComponentPortDirection.Slave;
            }

            return null;
        }
    }
}

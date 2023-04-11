using Avalonia.Data.Converters;
using Avalonia.Media;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Converters
{
    public class WireStatusToStrokeConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is WireStatus wireStatus)
            {
                return wireStatus switch
                {
                    WireStatus.Normal => new SolidColorBrush(Colors.Black),
                    WireStatus.Invalid => new SolidColorBrush(Colors.DarkRed),
                    WireStatus.Highlighted => new SolidColorBrush(Colors.DarkOrange),
                    WireStatus.Temporary => new SolidColorBrush(Colors.DarkGray),
                    _ => new SolidColorBrush(Colors.Red)
                };
            }

            return null;
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

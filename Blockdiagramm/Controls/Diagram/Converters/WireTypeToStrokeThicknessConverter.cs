using Avalonia.Data.Converters;
using Blockdiagramm.ViewModels.Diagram.Wire;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Converters
{
    public class WireTypeToStrokeThicknessConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            if (value is WireType wireType)
            {
                return wireType switch
                {
                    WireType.Single => 2,
                    WireType.Bus => 4,
                    _ => 1,
                };
            }

            return null;
        }
        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

using Avalonia;
using Avalonia.Data.Converters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Ribbon.Converters
{
    internal class LocationInListToMenuCornerRadiusConverter : IMultiValueConverter
    {
        private const double cornerSize = 3;

        private static CornerRadius GetCorner(bool first, bool last)
        {
            double topCorner = first ? cornerSize : 0;
            double bottomCorner = last ? cornerSize : 0;

            return new(topCorner, bottomCorner);
        }

        public object? Convert(IList<object?> values, Type targetType, object? parameter, CultureInfo culture)
        {
            if (values.Count == 2)
            {
                bool first = (bool?)values.First() ?? false;
                bool last = (bool?)values.Last() ?? false;
                return GetCorner(first, last);
            }

            return new CornerRadius(0);
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture) => throw new NotImplementedException();
    }
}

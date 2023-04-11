using Avalonia;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class PointExtension
    {
        public static (double, double) ToTuple(this Point point) => (point.X, point.Y);

        public static double Det(this Point point, Point other) => point.X * other.Y - point.Y * other.X;

        public static bool IsOnLine(this Point p1, Point p2, Point p3, double threshold = double.Epsilon)
        {
            Point vectorP12 = p2 - p1;
            Point vectorP23 = p3 - p2;
            double point = vectorP12.Det(vectorP23);

            return Math.Abs(point) < threshold;
        }
    }
}

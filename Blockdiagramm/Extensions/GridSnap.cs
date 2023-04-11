using Avalonia;
using System;

namespace Blockdiagramm.Extensions
{
    public static class GridSnap
    {
        public static Point SnapToRectGrid(Point point, double gridSize)
        {
            double x = Math.Round(point.X / gridSize) * gridSize;
            double y = Math.Round(point.Y / gridSize) * gridSize;
            return (x, y).ToPoint();
        }
    }
}

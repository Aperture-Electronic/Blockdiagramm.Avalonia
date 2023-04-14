using Avalonia;
using Avalonia.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Component
{
    public class ComponentMovedEventArgs : RoutedEventArgs
    {
        public Point PreviousPosition { get; }
        public Point NewPosition { get; }

        public ComponentMovedEventArgs(Point previousPosition, Point newPosition)
        {
            PreviousPosition = previousPosition;
            NewPosition = newPosition;
        }

    }
}

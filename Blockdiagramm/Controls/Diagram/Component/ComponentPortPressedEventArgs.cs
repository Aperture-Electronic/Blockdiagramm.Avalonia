using Avalonia;
using Avalonia.Interactivity;
using Blockdiagramm.ViewModels.Diagram.Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram.Component
{
    public class ComponentPortPressedEventArgs : RoutedEventArgs
    {
        /// <summary>
        /// The port model of the port that is pressed
        /// </summary>
        public ComponentPortModel PortModel { get; }

        /// <summary>
        /// The position of the port on the component
        /// </summary>
        public Point OnComponentPosition { get; set; }

        /// <summary>
        /// The part model of the component that contains the port
        /// </summary>
        public ComponentPartModel PartModel { get; set; } = null!;

        public ComponentPortPressedEventArgs(RoutedEvent e, ComponentPortModel portModel)
        {
            RoutedEvent = e;
            PortModel = portModel;
        }
    }
}

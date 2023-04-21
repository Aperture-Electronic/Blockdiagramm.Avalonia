using Avalonia;
using Avalonia.Interactivity;
using Blockdiagramm.Models;
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
        public ComponentPortModel PortModel
        {
            get
            {
                if (Port.DataContext is ComponentPortModel model)
                {
                    return model;
                }

                throw new Exception("The port has not a data context of port model");
            }
        }

        /// <summary>
        /// The port that is pressed
        /// </summary>
        public ComponentPort Port { get; }

        /// <summary>
        /// The part model of the component that contains the port
        /// </summary>
        public ComponentPartInstanceModel PartModel { get; set; } = null!;

        public ComponentPortPressedEventArgs(RoutedEvent e, ComponentPort port)
        {
            RoutedEvent = e;
            Port = port;
        }
    }
}

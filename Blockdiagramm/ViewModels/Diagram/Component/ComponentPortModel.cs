using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram.Component
{
    public class ComponentPortModel : INotifyPropertyChanged
    {
        #region Internal fields
        private ComponentPortDirection direction = ComponentPortDirection.Master;
        private string name = "";
        #endregion

        #region Readonly properties
        public static double PortStackSize => 10;

        public static double PortHeight => 20;
        private string DisplayName => name;
        #endregion

        #region Notify properties
        public ComponentPortDirection Direction
        {
            get => direction;
            set
            {
                if (direction == value)
                {
                    return;
                }

                direction = value;
                OnPropertyChanged(nameof(Direction));
            }
        }

        public string Name
        {
            get => name;
            set
            {
                if (name == value)
                {
                    return;
                }

                name = value;
                OnPropertyChanged(nameof(Name));

                // Also, notify changing of dependencies
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        public event PropertyChangedEventHandler? PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
            =>PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public ComponentPortModel(ComponentPortDirection direction, string name)
        {
            this.direction = direction;
            this.name = name;
        }

        // TODO
        public ComponentPortModel()
        {

        }
    }
}

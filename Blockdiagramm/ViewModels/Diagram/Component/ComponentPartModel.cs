using Avalonia;
using Avalonia.Collections;
using Blockdiagramm.Extensions;
using DynamicData;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram.Component
{
    public class ComponentPartModel : INotifyPropertyChanged
    {
        #region Internal fields
        private string name = "";
        private string instanceName = "";
        private readonly AvaloniaList<ComponentPortModel> ports = new AvaloniaList<ComponentPortModel>();
        #endregion

        #region Readonly properties
        private static double PortHorizontalMargin => 5;

        private static double ComponentMargin => 10;

        private static double PortVerticalMargin => 20 + ComponentMargin;

        public IEnumerable<ComponentPortModel> SlavePorts => ports.Where(p => p.Direction == ComponentPortDirection.Slave);
        public IEnumerable<ComponentPortModel> MasterPorts => ports.Where(p => p.Direction == ComponentPortDirection.Master);
        #endregion

        #region Notify properties
        public string Name
        {
            get => name;
            set => NotifyProperty.ChangeProperty(ref name, value, nameof(Name), OnPropertyChanged);
        }

        public string InstanceName
        {
            get => instanceName;
            set => NotifyProperty.ChangeProperty(ref instanceName, value, nameof(InstanceName), OnPropertyChanged);
        }

        public AvaloniaList<ComponentPortModel> Ports => ports;

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public ComponentPartModel() => ports.CollectionChanged += OnPortsCollectionChanged;

        private void OnPortsCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            void NotifyPropertyForList(IList ports)
            {
                int count = ports.Count;
                if (count == 0)
                {
                    return;
                }

                IEnumerable<ComponentPortModel> slaveQuery = ports.OfType<ComponentPortModel>().Where(p => p.Direction == ComponentPortDirection.Slave);
                int slaveCount = slaveQuery.Count();

                if (slaveCount == 0)
                {
                    // The new list is full of master ports
                    OnPropertyChanged(nameof(MasterPorts));
                }
                else if (slaveCount < count)
                {
                    // The new list has both master and slave ports
                    OnPropertyChanged(nameof(SlavePorts));
                    OnPropertyChanged(nameof(MasterPorts));
                }
                else
                {
                    // The new list is full of slave ports
                    OnPropertyChanged(nameof(SlavePorts));
                }
            }

            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                if (e.NewItems != null)
                {
                    NotifyPropertyForList(e.NewItems);
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Reset)
            {
                OnPropertyChanged(nameof(SlavePorts));
                OnPropertyChanged(nameof(MasterPorts));
            }
            else
            {
                if (e.OldItems != null)
                {
                    NotifyPropertyForList(e.OldItems);
                }

                if (e.NewItems != null)
                {
                    NotifyPropertyForList(e.NewItems);
                }
            }
        }
    
        public Point GetPortPosition(ComponentPortModel portModel, Rect partBound)
        {
            if (!ports.Contains(portModel))
            {
                throw new Exception("No such port in the part");
            }

            int index = portModel.Direction == ComponentPortDirection.Master ? 
                MasterPorts.IndexOf(portModel) : 
                SlavePorts.IndexOf(portModel);

            double y = PortVerticalMargin + index * ComponentPortModel.PortHeight;
            double x = portModel.Direction == ComponentPortDirection.Master ?
                       partBound.Width - PortHorizontalMargin - 1 :
                       PortHorizontalMargin;

            return (x, y).ToPoint();
        }
    }
}

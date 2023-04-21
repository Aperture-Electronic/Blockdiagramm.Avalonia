using Avalonia;
using Blockdiagramm.Extensions;
using Blockdiagramm.Models;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Diagram.Component
{
    /// <summary>
    /// The component part instance for display in the diagram
    /// </summary>
    public class ComponentPartInstanceModel : ViewModelBase, IPartModel<ComponentPortModel>
    {
        #region Internal fields
        private readonly ReadOnlyObservableCollection<ComponentPortModel> slavePorts;
        private readonly ReadOnlyObservableCollection<ComponentPortModel> masterPorts;
        private string instanceName;
        private readonly ObservableAsPropertyHelper<string> name;
        #endregion

        public ComponentPartModel PartModel { get; }
        public ReadOnlyObservableCollection<ComponentPortModel> SlavePorts => slavePorts;
        public ReadOnlyObservableCollection<ComponentPortModel> MasterPorts => masterPorts;

        public string Name => name.Value;

        #region Static properties
        private static double LabelHorizontalMargin => 20;

        private static double PortHorizontalMargin => 5;

        private static double ComponentMargin => 10;

        private static double PortVerticalMargin => 20;

        private static double LabelVerticalMargin => 0;
        #endregion

        /// <summary>
        /// The name of the instance
        /// </summary>
        public string InstanceName
        {
            get => instanceName;
            set => this.RaiseAndSetIfChanged(ref instanceName, value);
        }
        
        public ComponentPartInstanceModel(ComponentPartModel partModel, string instanceName)
        {
            PartModel = partModel;
            this.instanceName = instanceName;

            // Bind name
            this.WhenAnyValue(p => p.PartModel.Name).ToProperty(this, nameof(Name), out name);

            // Bind ports
            PartModel.Ports.Connect().Filter(p => p.Direction == PortDirection.Slave)
                .Bind(out slavePorts).Subscribe();
            PartModel.Ports.Connect().Filter(p => p.Direction == PortDirection.Master)
                .Bind(out masterPorts).Subscribe();
        }

        public Point GetPortPosition(IPortModel portModel, Rect partBound)
        {
            if (portModel is not ComponentPortModel model || !PartModel.Ports.Items.Contains(model))
            {
                throw new Exception("No such port in the part");
            }

            int index = model.Direction == PortDirection.Master ?
                MasterPorts.IndexOf(portModel) :
                SlavePorts.IndexOf(portModel);

            double y = ComponentMargin + PortVerticalMargin + index * ComponentPortModel.PortHeight;
            double x = model.Direction == PortDirection.Master ?
                       partBound.Width - PortHorizontalMargin - 1 :
                       PortHorizontalMargin;

            return (x, y).ToPoint();
        }
    }
}

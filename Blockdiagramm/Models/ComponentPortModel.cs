using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using Blockdiagramm.ViewModels.Diagram;
using ReactiveUI;

namespace Blockdiagramm.Models
{
    public class ComponentPortModel : ReactiveObject, IPortModel
    {
        #region Internal fields
        private PortDirection direction = PortDirection.Master;
        private string name = "";
        private readonly ObservableAsPropertyHelper<string> displayName;
        #endregion

        #region Static properties
        public static double PortStackSize => 10;

        public static double PortHeight => 20;
        #endregion

        #region Readonly properties
        private string DisplayName => displayName.Value;
        #endregion

        #region Notify properties
        public PortDirection Direction
        {
            get => direction;
            set => this.RaiseAndSetIfChanged(ref direction, value); 
        }

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);  
        }
        #endregion

        public ComponentPortModel(PortDirection direction, string name)
        {
            this.direction = direction;
            this.name = name;

            // TODO
            this.WhenAnyValue(x => x.Name).Select(name => name).ToProperty(this, nameof(Name), out displayName);
        }
    }
}

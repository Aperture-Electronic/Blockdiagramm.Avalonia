using Avalonia;
using Avalonia.Collections;
using Blockdiagramm.Extensions;
using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels.Diagram;
using DynamicData;
using HDLElaborateRoslyn.Elaborated;
using ReactiveUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Models
{
    public class ComponentPartModel : ReactiveObject
    {
        #region Internal fields
        private string name = "";
        #endregion

        public SourceFile SourceFile { get; }

        #region Public properties
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public SourceList<ComponentPortModel> Ports { get; } = new();

        #endregion

        public ComponentPartModel(SourceFile file, ElaboratedModule module) : this(file)
        {
            UpdateModule(module);
        }

        public ComponentPartModel(SourceFile file) 
        {
            SourceFile = file;
        }

        public void UpdateModule(ElaboratedModule module)
        {
            InitializePorts(module);
        }

        private void InitializePorts(ElaboratedModule module)
        {
            // TODO
            foreach (var port in module.ElaboratedModulePorts)
            {
                Ports.Add(new ComponentPortModel(port.Direction == HDLAbstractSyntaxTree.Types.Direction.In ? PortDirection.Slave : PortDirection.Master,
                    port.Name));
            }
        }
    }
}

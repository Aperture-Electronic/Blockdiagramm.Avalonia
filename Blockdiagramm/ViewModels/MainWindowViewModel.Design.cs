using Blockdiagramm.Models;
using Blockdiagramm.ViewModels.Diagram.Component;
using Blockdiagramm.ViewModels.Dialogues;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public partial class MainWindowViewModel
    {
        public ICommand AddComponentCommand { get; }

        public Interaction<string, AddComponentDialogViewModel> ShowAddComponentDialog { get; } = new();

        public async Task AddComponentTask()
        {
            if (ComponentListViewModel.SelectedItem == null)
            {
                return;
            }

            ComponentPartModel targetComponent = ComponentListViewModel.SelectedItem;

            // TODO: Generate a default instance name for the new component
            string defaultName = ComponentListViewModel.SelectedItem.Name + "1";

            // Show the AddComponentDialog and wait for the result
            AddComponentDialogViewModel result = await ShowAddComponentDialog.Handle(defaultName);

            // If the user cancelled the dialog, return            
            if (result == null)
            {
                return;
            }

            // TODO: Check if the instance name is already in use

            // Create new instance of the component, and add it to the diagram
            ComponentPartInstanceModel instanceModel = new(targetComponent, result.InstanceName);
            CurrentDiagram.AddComponent(instanceModel);
        }
    }
}

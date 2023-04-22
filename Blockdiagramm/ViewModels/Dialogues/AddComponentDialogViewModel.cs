using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels.Dialogues
{
    public class AddComponentDialogViewModel : DialogueViewModelBase
    {
        #region Private Fields
        private string instanceName;
        private bool instanceNameInvalid;
        private string instanceNameInvalidReason;
        #endregion

        #region Commands
        public ICommand ConfirmAddComponentCommand { get; }
        #endregion

        #region Interactions
        public Interaction<Unit, bool> ConfirmAddComponent { get; } = new();
        #endregion

        public string InstanceName
        {
            get => instanceName;
            set
            {
                if (instanceName != value)
                {
                    InstanceNameInvalid = CheckInstanceNameInvalid(value, out string reason);
                    InstanceNameInvalidReason = reason;
                }

                this.RaiseAndSetIfChanged(ref instanceName, value);
            }
        }

        public bool InstanceNameInvalid
        {
            get => instanceNameInvalid;
            set => this.RaiseAndSetIfChanged(ref instanceNameInvalid, value);
        }

        public string InstanceNameInvalidReason
        {
            get => instanceNameInvalidReason;
            set => this.RaiseAndSetIfChanged(ref instanceNameInvalidReason, value);
        }

        public bool ViewModelValid => !InstanceNameInvalid;

        public AddComponentDialogViewModel(string defaultName = "")
        {
            ConfirmAddComponentCommand = ReactiveCommand.CreateFromTask(ConfirmAddComponentTask);
            instanceName = defaultName;
            instanceNameInvalid = false;
            instanceNameInvalidReason = "";
        }

        public void Validate()
        {
            InstanceNameInvalid = CheckInstanceNameInvalid(InstanceName, out string reason);
            InstanceNameInvalidReason = reason;
        }

        public async Task ConfirmAddComponentTask()
        {
            var result = await ConfirmAddComponent.Handle(Unit.Default);
            if (result)
            {
                // Nothing to do
            }
        }

        private static bool CheckInstanceNameInvalid(string instanceName, out string reason)
        {
            // The instance name must not be empty
            if (string.IsNullOrWhiteSpace(instanceName))
            {
                reason = "Required";
                return true;
            }

            // The instance name can only contains letters, numbers and underscores
            if (!instanceName.All(c => char.IsLetterOrDigit(c) || c == '_'))
            {
                reason = "Only letters, numbers and underscores are allowed";
                return true;
            }

            // The instance name must not start with a number
            if (char.IsDigit(instanceName[0]))
            {
                reason = "The instance name must not start with a number";
                return true;
            }

            reason = "";
            return false;
        }
    }
}

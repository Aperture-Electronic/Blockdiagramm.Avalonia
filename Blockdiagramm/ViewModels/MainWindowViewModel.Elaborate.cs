using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels.Dialogues;
using Blockdiagramm.Views.Dialogues;
using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public partial class MainWindowViewModel
    {
        public ICommand ElaborateAllCommand { get; }

        private async Task ElaborateAllTask()
        {
            ProcessingWaitDialogViewModel procWaitDialogModel = new()
            {
                Title = "Elaborating...",
                Icon = "mdi-data-matrix-scan",
            };

            // Show the waiting dialog by calling the interaction
            // and get the dialog instance for close it after elaborate
            ProcessingWaitDialog dialog = await ShowProcessingWait.Handle(procWaitDialogModel);

            // Execute the elaboration
            bool isExceiption = false;
            try
            {
                // Create a new thread for elaborate
                await Task.Run(GlobalStatic.Project.ElaborateAll);
            }
            catch (Exception ex)
            {
                // Close the waiting dialog when exception occoured
                dialog.Close();

                isExceiption = true;

                // Show the exception
                ExceptionErrorDialogViewModel exceptionViewModel = new(ex.Message, "Elaborate");
                await ShowException.Handle(exceptionViewModel);
            }
            finally
            {
                if (!isExceiption)
                {
                    // Close the dialog when done
                    dialog.Close();
                }
            }
        }
    }
}

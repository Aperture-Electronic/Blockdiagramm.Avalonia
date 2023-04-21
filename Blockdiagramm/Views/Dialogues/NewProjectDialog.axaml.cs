using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.ReactiveUI;
using Blockdiagramm.Models;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Dialogues;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Reactive;
using System.Threading.Tasks;

namespace Blockdiagramm.Views.Dialogues
{
    public partial class NewProjectDialog : DialogBase<NewProjectDialogViewModel>
    {
        public NewProjectDialog()
        {
            InitializeComponent();

            this.WhenActivated(d =>
            {
                RegisterBaseHandlers(d);

                d(ViewModel!.BrowsePath.RegisterHandler(BrowsePathAsync));
                d(ViewModel!.ConfirmCreateProject.RegisterHandler(ConfirmCreateProject));
            });
        }

        private async Task BrowsePathAsync(InteractionContext<string, (string, bool)> args)
        {
            OpenFolderDialog ofd = new()
            {
                Directory = args.Input,
                Title = "Select a folder to store the project"
            };
                
            var result = await ofd.ShowAsync(this);

            if (result != null)
            {
                args.SetOutput((result, true));
                return;
            }

            args.SetOutput(("", false));
        }

        private void ConfirmCreateProject(InteractionContext<Unit, bool> args)
        {
            if (DataContext is NewProjectDialogViewModel model)
            {
                model.Validate(); // Validate the fields

                if (model.ViewModelValid)
                {
                    Submit();
                    args.SetOutput(true);
                    return;
                }
                else
                {
                    args.SetOutput(false);
                    return;
                }
            }

            args.SetOutput(false);
        }

        private void Submit() => Close(DataContext);
    }
}

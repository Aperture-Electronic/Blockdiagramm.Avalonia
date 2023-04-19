using Blockdiagramm.Logic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels.Dialogues
{
    public class AddSourceFileDialogViewModel : DialogueViewModelBase
    {
        #region Private Fields
        private string sourceFilePaths;
        private SourceFileType sourceFileType;

        private bool sourceFilePathsInvalid;

        private string sourceFilePathsInvalidReason;

        public ICommand BrowseFilesCommand { get; }
        public ICommand ConfirmAddSourceCommand { get; }
        #endregion

        #region Interactions
        public Interaction<string, (string, bool, SourceFileType)> BrowseFiles { get; } = new();

        public Interaction<object?, bool> ConfirmAddSource { get; } = new();
        #endregion
        
        public string SourceFilePaths
        {
            get => sourceFilePaths;
            set
            {
                if (sourceFilePaths != value)
                {
                    SourceFilePathsInvalid = CheckSourceFilePathsInvalid(value, out string reason);
                    SourceFilePathsInvalidReason = reason;
                }

                this.RaiseAndSetIfChanged(ref sourceFilePaths, value);
            }
        }

        public SourceFileType SourceFileType
        {
            get => sourceFileType;
            set => this.RaiseAndSetIfChanged(ref sourceFileType, value);
        }

        public bool SourceFilePathsInvalid
        {
            get => sourceFilePathsInvalid;
            private set => this.RaiseAndSetIfChanged(ref sourceFilePathsInvalid, value);
        }

        public string SourceFilePathsInvalidReason 
        { 
            get => sourceFilePathsInvalidReason;
            private set => this.RaiseAndSetIfChanged(ref sourceFilePathsInvalidReason, value);
        }

        public bool ViewModelValid => !SourceFilePathsInvalid;

        public string[] SourceFilePathsArray => SourceFilePaths.Split(';');

        public AddSourceFileDialogViewModel()
        {
            BrowseFilesCommand = ReactiveCommand.CreateFromTask(BrowseFilesAsync);
            ConfirmAddSourceCommand = ReactiveCommand.CreateFromTask(ConfirmAddSourceAsync);

            sourceFilePaths = "";
            sourceFileType = SourceFileType.Auto;
            sourceFilePathsInvalidReason = "";
        }

        private static bool CheckSourceFilePathsInvalid(string sourceFilePaths, out string reason)
        {
            if (string.IsNullOrWhiteSpace(sourceFilePaths))
            {
                reason = "Required";
                return true;
            }

            reason = "";
            return false;
        }

        public void Validate()
        {
            SourceFilePathsInvalid = CheckSourceFilePathsInvalid(SourceFilePaths, out string reason);
            SourceFilePathsInvalidReason = reason;
        }

        private async Task BrowseFilesAsync()
        {
            string firstFileLocation = "";
            if (!string.IsNullOrWhiteSpace(sourceFilePaths))
            {
                string[] files = sourceFilePaths.Split(';');
                string firstFile = files.First();
                firstFileLocation = System.IO.Path.GetDirectoryName(firstFile) ?? "";
            }

            var (filePaths, success, type) = await BrowseFiles.Handle(firstFileLocation);

            if (success)
            {
                SourceFilePaths = filePaths;
                SourceFileType = type;
            }
        }

        private async Task ConfirmAddSourceAsync()
        {
            var success = await ConfirmAddSource.Handle(null);
            if (success)
            {
                // Nothing to do
            }
        }
    }
}

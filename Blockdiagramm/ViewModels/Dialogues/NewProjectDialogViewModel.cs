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
    public class NewProjectDialogViewModel : DialogueViewModelBase
    {
        private string projectName;
        private string path;

        private bool projectNameInvalid;
        private bool pathInvalid;

        private string projectNameInvalidReason;
        private string pathInvalidReason;

        public ICommand BrowsePathCommand { get; }

        public ICommand ConfirmCreateProjectCommand { get; }

        public Interaction<string, (string, bool)> BrowsePath { get; } = new();

        public Interaction<object?, bool> ConfirmCreateProject { get; } = new();

        public string ProjectName
        {
            get => projectName;
            set
            {
                if (projectName != value)
                {
                    ProjectNameInvalid = CheckProjectNameInvalid(value, out string reason);
                    ProjectNameInvalidReason = reason;
                }

                this.RaiseAndSetIfChanged(ref projectName, value);
            }
        }

        public string Path
        {
            get => path;
            set
            {
                if (path != value)
                {
                    PathInvalid = CheckPathInvalid(value, out string reason);
                    PathInvalidReason = reason;
                }

                this.RaiseAndSetIfChanged(ref path, value);
            }
        }

        public bool ProjectNameInvalid
        {
            get => projectNameInvalid;
            private set => this.RaiseAndSetIfChanged(ref projectNameInvalid, value);
        }

        public bool PathInvalid
        {
            get => pathInvalid;
            private set => this.RaiseAndSetIfChanged(ref pathInvalid, value);
        }

        public string ProjectNameInvalidReason
        {
            get => projectNameInvalidReason;
            private set => this.RaiseAndSetIfChanged(ref projectNameInvalidReason, value);
        }

        public string PathInvalidReason
        {
            get => pathInvalidReason;
            private set => this.RaiseAndSetIfChanged(ref pathInvalidReason, value);
        }

        public bool ViewModelValid => !PathInvalid && !ProjectNameInvalid;


        private static bool CheckProjectNameInvalid(string value, out string reason)
        {
            // The value must not be empty
            if (string.IsNullOrWhiteSpace(value))
            {
                reason = "Required";
                return true;
            }

            // The value must not contains white spaces
            int length = value.Length;
            for (int i = 0; i < length; i++)
            {
                if (char.IsWhiteSpace(value[i]))
                {
                    reason = "Must not contains white spaces";
                    return true;
                }
            }

            // The value must not contains invalid characters
            if (value.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                reason = "Must not contains invalid characters";
                return true;
            }

            reason = "";
            return false;
        }

        private static bool CheckPathInvalid(string value, out string reason)
        {
            // The path must not be empty
            if (string.IsNullOrWhiteSpace(value))
            {
                reason = "Required";
                return true;
            }

            reason = "";
            return false;
        }

        public void Validate()
        {
            ProjectNameInvalid = CheckProjectNameInvalid(ProjectName, out string projectNameInvalidReason);
            PathInvalid = CheckPathInvalid(Path, out string pathInvalidReason);

            ProjectNameInvalidReason = projectNameInvalidReason;
            PathInvalidReason = pathInvalidReason;
        }

        public NewProjectDialogViewModel()
        {
            BrowsePathCommand = ReactiveCommand.CreateFromTask(async () =>
            {
                (string path, bool valid) = await BrowsePath.Handle(Path);
                if (valid)
                {
                    Path = path;
                }
            });

            ConfirmCreateProjectCommand = ReactiveCommand.CreateFromTask(async () => 
            {
                var success = await ConfirmCreateProject.Handle(null);
                if (!success)
                {
                    // Nothing to do
                }
            });

            projectName = "";
            path = "";
            projectNameInvalidReason = "";
            pathInvalidReason = "";
        }
    }
}

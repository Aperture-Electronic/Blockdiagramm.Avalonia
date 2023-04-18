using Avalonia;
using Avalonia.Collections;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public class Project : INotifyPropertyChanged
    {
        #region Notify Property Changed
        public event PropertyChangedEventHandler? PropertyChanged;

        /// <summary>
        /// Let the UI know that a property has changed
        /// </summary>
        public void UpdateProperty(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        #region Private fields
        private string path = "";
        private string name = "";
        #endregion

        #region Public readonly properties
        public AvaloniaList<SourceFile> SourceFiles { get; } = new();
        #endregion

        /// <summary>
        /// Path to store project files
        /// </summary>
        public string Path
        {
            get => path;
            set => NotifyProperty.ChangeProperty(ref path, value, nameof(Path), UpdateProperty);
        }

        /// <summary>
        /// Project name
        /// </summary>
        public string Name
        {
            get => name;
            set => NotifyProperty.ChangeProperty(ref name, value, nameof(Name), UpdateProperty);
        }

        /// <summary>
        /// Validation of project
        /// </summary>
        public bool IsValid => !string.IsNullOrEmpty(Path);

        public Project()
        {

        }

        /// <summary>
        /// Initialize a new project by name and path
        /// </summary>
        /// <param name="projectName">Project name</param>
        /// <param name="projectPath">Path</param>
        public void NewProject(string projectName, string projectPath)
        {
            if (string.IsNullOrWhiteSpace(projectName) || string.IsNullOrWhiteSpace(projectPath))
            {
                throw new Exception($"{nameof(projectName)} and {nameof(projectPath)} can not be empty");
            }

            Name = projectName;
            Path = projectPath;

            // Clear the list of project items
            SourceFiles.Clear();

            // Update the valid property
            UpdateProperty(nameof(IsValid));
        }

        /// <summary>
        /// Close current project if it is valid
        /// </summary>
        public void CloseProject()
        {
            if (IsValid)
            {
                Path = string.Empty;
                Name = string.Empty;

                // Update the valid property
                UpdateProperty(nameof(IsValid));
            }
        }
    }
}

using Avalonia;
using Avalonia.Collections;
using Blockdiagramm.Extensions;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    [Serializable, DataContract(Name = "Project", Namespace = "www.blockdiagramm.org")]
    public partial class Project : ReactiveObject
    {
        #region Private fields
        private string path = "";
        private string name = "";
        private ObservableAsPropertyHelper<bool> isValid;
        #endregion

        /// <summary>
        /// Path to store project files
        /// </summary>
        [DataMember]
        public string Path
        {
            get => path;
            set => this.RaiseAndSetIfChanged(ref path, value);
        }

        /// <summary>
        /// Project name
        /// </summary>
        [DataMember]
        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        /// <summary>
        /// Validation of project
        /// </summary>
        public bool IsValid => isValid.Value;

        public Project()
        {
            this.WhenAnyValue(x => x.Path, e => !string.IsNullOrWhiteSpace(e)).ToProperty(this, nameof(IsValid), out isValid);
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
            }
        }
    }
}

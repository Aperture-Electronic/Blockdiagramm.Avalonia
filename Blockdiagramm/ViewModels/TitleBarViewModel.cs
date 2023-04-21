using Blockdiagramm.Logic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public class TitleBarViewModel : ViewModelBase
    {
        public IWindowBaseViewModel WindowModel { get; }

        public string Version { get; }
        public Project Project { get; }

        public TitleBarViewModel(IWindowBaseViewModel model, string version, Project project)
        {
            WindowModel = model;
            Version = version;
            Project = project;
        }
    }
}

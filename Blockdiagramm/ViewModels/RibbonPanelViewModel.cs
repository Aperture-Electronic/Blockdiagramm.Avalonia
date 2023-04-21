using Blockdiagramm.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public class RibbonPanelViewModel : ViewModelBase
    {
        private Project project;

        public MainWindowViewModel WindowModel { get; }

        public Project Project => project;
        
        public RibbonPanelViewModel(MainWindowViewModel windowModel, Project project)
        {
            WindowModel = windowModel;
            this.project = project;
        }
    }
}

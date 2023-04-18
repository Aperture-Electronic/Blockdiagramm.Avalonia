using Blockdiagramm.Logic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public class RibbonPanelViewModel : INotifyPropertyChanged
    {
        #region Notify Property Changed
        public event PropertyChangedEventHandler? PropertyChanged;

        public void UpdateProperty(string propertyName)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        #endregion

        public MainWindowViewModel WindowModel { get; }

        public Project Project { get; }
        
        public RibbonPanelViewModel(MainWindowViewModel windowModel, Project project)
        {
            WindowModel = windowModel;
            Project = project;
        }
    }
}

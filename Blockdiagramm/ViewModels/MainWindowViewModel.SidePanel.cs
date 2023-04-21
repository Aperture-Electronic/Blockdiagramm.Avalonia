using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
    public partial class MainWindowViewModel
    {
        private int selectedSidePanelIndex = 0;

        public ICommand SelectSidePanelCommand { get; }

        /// <summary>
        /// Indicate which side panel is selected
        /// </summary>
        public int SelectedSidePanelIndex
        {
            get => selectedSidePanelIndex;
            set => this.RaiseAndSetIfChanged(ref selectedSidePanelIndex, value);
        }
    }
}

using Blockdiagramm.Models;
using Blockdiagramm.ViewModels.Dialogues;
using Blockdiagramm.Views.Dialogues;
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
        public ICommand NewProjectCommand { get; }

        public Interaction<object?, NewProjectDialogViewModel> NewProject { get; } = new();
    }
}

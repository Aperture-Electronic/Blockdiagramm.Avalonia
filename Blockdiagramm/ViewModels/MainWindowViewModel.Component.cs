using Blockdiagramm.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels
{
    public partial class MainWindowViewModel
    {
        public FilterListViewModel<ComponentPartModel> ComponentListViewModel { get; }
    }
}

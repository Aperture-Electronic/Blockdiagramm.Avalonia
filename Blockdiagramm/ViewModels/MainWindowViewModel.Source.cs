using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels.Dialogues;
using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
	public partial class MainWindowViewModel
	{
		public ICommand AddSourceFileCommand { get; }

		public Interaction<object?, AddSourceFileDialogViewModel> AddSourceFile { get; } = new();

		private async Task AddSourceTask()
		{
			AddSourceFileDialogViewModel result = await AddSourceFile.Handle(null);
			if (result?.ViewModelValid ?? false)
			{
				try
				{
					GlobalStatic.Project.AddSources(result.SourceFilePathsArray, result.SourceFileType);
				}
				catch (Exception ex)
				{
					throw;
                }
            }
		}
    }
}

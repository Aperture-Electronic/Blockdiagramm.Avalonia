using Blockdiagramm.Logic;
using Blockdiagramm.ViewModels.Dialogues;
using Blockdiagramm.Views.Dialogues;
using Microsoft.VisualBasic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Blockdiagramm.ViewModels
{
	public partial class MainWindowViewModel
	{
		public ICommand AddSourceFileCommand { get; }

		public Interaction<Unit, AddSourceFileDialogViewModel> AddSourceFile { get; } = new();

		public FilterListViewModel<SourceFile> SourceFileListViewModel { get; }

        private async Task AddSourceTask()
		{
			AddSourceFileDialogViewModel result = await AddSourceFile.Handle(Unit.Default);
			if (result?.ViewModelValid ?? false)
			{
				try
				{
					GlobalStatic.Project.AddSources(result.SourceFilePathsArray, result.SourceFileType);
				}
				catch (Exception ex)
				{
					ExceptionErrorDialogViewModel model = new(ex.Message, "Add Source");
					await ShowException.Handle(model);
                }
            }
		}
    }
}

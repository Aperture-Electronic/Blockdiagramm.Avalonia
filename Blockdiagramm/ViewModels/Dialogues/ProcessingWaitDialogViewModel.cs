using Avalonia.Media;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.ViewModels.Dialogues
{
    public class ProcessingWaitDialogViewModel : DialogueViewModelBase
    {
        private bool isIndeterminate = false;
        private readonly ObservableAsPropertyHelper<int>? value = null;
        private readonly ObservableAsPropertyHelper<int>? maximum = null;
        private readonly int fixedMaximum = 0;
        private string title = "";
        private string icon = "";
        private IBrush titleBarBackground = new SolidColorBrush(0xFF802040);

        public bool IsIndeterminate
        {
            get => isIndeterminate;
            set => this.RaiseAndSetIfChanged(ref isIndeterminate, value);
        }

        public string Title
        {
            get => title;
            set => this.RaiseAndSetIfChanged(ref title, value);
        }

        public string Icon
        {
            get => icon;
            set => this.RaiseAndSetIfChanged(ref icon, value);
        }   

        public IBrush TitleBarBackground
        {
            get => titleBarBackground;
            set => this.RaiseAndSetIfChanged(ref titleBarBackground, value);
        }

        public int Value => value?.Value ?? 0;

        public int Maximum => maximum?.Value ?? fixedMaximum;

        public ProcessingWaitDialogViewModel()
        {
            isIndeterminate = true;
        }

        public ProcessingWaitDialogViewModel(IObservable<int> value, int fixedMaximum)
        {
            isIndeterminate = false;
            this.fixedMaximum = fixedMaximum;
            value.ToProperty(this, nameof(Value), out this.value);
        }

        public ProcessingWaitDialogViewModel(IObservable<int> value, IObservable<int> maximum)
        {
            isIndeterminate = false;
            value.ToProperty(this, nameof(Value), out this.value);
            maximum.ToProperty(this, nameof(Maximum), out this.maximum);
        }
    }
}

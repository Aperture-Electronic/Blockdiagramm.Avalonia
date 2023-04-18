using Avalonia;
using Blockdiagramm.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Views
{
    public class DialogBase<T> : DiagrammeWindowBase<T> where T : class, IWindowBaseViewModel
    {
        public static readonly StyledProperty<string> DialogTitleProperty
            = AvaloniaProperty.Register<DialogBase<T>, string>(nameof(DialogTitle));

        public string DialogTitle
        {
            get => GetValue(DialogTitleProperty);
            set => SetValue(DialogTitleProperty, value);
        }
    }
}

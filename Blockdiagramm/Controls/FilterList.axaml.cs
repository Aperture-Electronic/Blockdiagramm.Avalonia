using Avalonia;
using Avalonia.Controls;

namespace Blockdiagramm.Controls
{
    public partial class FilterList : UserControl
    {
        public readonly static StyledProperty<object> ItemTemplateProperty =
            AvaloniaProperty.Register<FilterList, object>(nameof(ItemTemplate));

        public object ItemTemplate
        {
            get => GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public FilterList()
        {
            InitializeComponent();
        }
    }
}

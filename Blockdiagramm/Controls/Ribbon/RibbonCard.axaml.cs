using Avalonia;
using Avalonia.Controls;
using Avalonia.Data;

namespace Blockdiagramm.Controls.Ribbon
{
    public partial class RibbonCard : UserControl
    {
        public static readonly StyledProperty<string> TitleProperty =
            AvaloniaProperty.Register<RibbonCard, string>(nameof(Title), "Empty",
                defaultBindingMode: BindingMode.OneWay,
                coerce: CoerceTitle);

        public static readonly StyledProperty<object> CardContentProperty =
            AvaloniaProperty.Register<RibbonCard, object>(nameof(CardContent));

        private static string CoerceTitle(IAvaloniaObject instance, string value)
            => string.IsNullOrWhiteSpace(value) ? "Empty" : value;

        public string Title
        {
            get => GetValue(TitleProperty);
            set => SetValue(TitleProperty, value);
        }

        public object CardContent
        {
            get => GetValue(CardContentProperty);
            set => SetValue(CardContentProperty, value);
        }

        public RibbonCard()
        {
            InitializeComponent();
        }
    }
}

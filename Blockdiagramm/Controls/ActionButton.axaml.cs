using Avalonia;
using Avalonia.Controls;
using ReactiveUI;

namespace Blockdiagramm.Controls
{
    public partial class ActionButton : UserControl
    {
        public readonly static StyledProperty<string> TextProperty = 
            AvaloniaProperty.Register<ActionButton, string>(nameof(Text), "Button");

        public readonly static StyledProperty<IReactiveCommand> CommandProperty =
            AvaloniaProperty.Register<ActionButton, IReactiveCommand>(nameof(Command));

        public string Text
        {
            get => GetValue(TextProperty);
            set => SetValue(TextProperty, value);
        }   

        public IReactiveCommand Command
        {
            get => GetValue(CommandProperty);
            set => SetValue(CommandProperty, value);
        }

        public ActionButton()
        {
            InitializeComponent();
        }
    }
}

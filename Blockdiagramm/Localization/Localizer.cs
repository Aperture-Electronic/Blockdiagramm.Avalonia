using Avalonia;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Localization
{
    public class Localizer
    {
        public static AvaloniaProperty<string> LanguageProperty
            = AvaloniaProperty.RegisterAttached<AvaloniaObject, string>("Language", typeof(AvaloniaObject));

        public static void SetLanguage(AvaloniaObject obj, string language)
            => obj.SetValue(LanguageProperty, language);

        public static string? GetLanguage(AvaloniaObject obj)
            => obj.GetValue(LanguageProperty) as string;

        public string CurrentLanguage { get; private set; }

        public bool LoadLanguage(string language)
        {
            CurrentLanguage = language;
        }
    }
}

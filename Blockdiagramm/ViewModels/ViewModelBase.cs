using Avalonia;
using Avalonia.Markup.Xaml.MarkupExtensions;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blockdiagramm.ViewModels
{
    public class ViewModelBase : ReactiveObject
    {
        private const string LocalizationDictionary = "Localization";

        public void Translate(string targetLanguage = "en-US")
        {
            if (Application.Current == null) return;
            Application app = Application.Current;
            IEnumerable<ResourceInclude> resources = app.Resources.MergedDictionaries.OfType<ResourceInclude>();

            // Check old localization and remove it
            ResourceInclude? oldLocalization = resources.FirstOrDefault(x => x.Source?.OriginalString?.Contains('/' + LocalizationDictionary) ?? false);

            if (oldLocalization != null)
            {
                app.Resources.MergedDictionaries.Remove(oldLocalization);
            }

            // Add new localization for the language
            app.Resources.MergedDictionaries.Add(new ResourceInclude()
            {
                Source = new Uri($"avares://{app.Name}/Assets/{LocalizationDictionary}/{targetLanguage}.axaml")
            });
        }
    
        public static void SetDefaultLanguage()
        {
            string language = System.Globalization.CultureInfo.CurrentCulture.Name;

            // TODO
            language = "en-US";

            if (Application.Current == null) return;
            Application app = Application.Current;

            // Add new localization for the language
            app.Resources.MergedDictionaries.Add(new ResourceInclude()
            {
                Source = new Uri($"avares://{app.Name}/Assets/{LocalizationDictionary}/{language}.axaml")
            });
        }
    }
}
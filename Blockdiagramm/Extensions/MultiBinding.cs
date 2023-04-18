using Avalonia.Data;
using Avalonia.Data.Converters;
using Avalonia.Markup.Xaml;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public class MultiBinding : MarkupExtension
    {
        private readonly Avalonia.Data.MultiBinding multiBinding = new();

        public MultiBinding(BindingBase b1, BindingBase b2, object converter)
        { 
            multiBinding.Bindings.Add(b1);
            multiBinding.Bindings.Add(b2);
            multiBinding.Converter = converter as IMultiValueConverter;
        }

        public override object ProvideValue(IServiceProvider serviceProvider) => multiBinding;
    }
}

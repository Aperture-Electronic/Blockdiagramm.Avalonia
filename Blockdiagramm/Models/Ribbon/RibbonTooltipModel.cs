using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Models.Ribbon
{
    public class RibbonTooltipModel
    {
        public string Title { get; set; }
        public object Content { get; set; }

        public RibbonTooltipModel()
        {
            Title = "Tooltip Title";
            Content = "Tooltip Text";
        }

        public RibbonTooltipModel(string title, object content)
        {
            Title = title;
            Content = content;
        }
    }
}

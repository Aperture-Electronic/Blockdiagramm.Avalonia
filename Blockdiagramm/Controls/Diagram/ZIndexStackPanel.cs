using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Layout;
using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public class ZIndexStackPanel : StackPanel
    {
        protected override Size ArrangeOverride(Size finalSize)
        {
            bool fHorizontal = (Orientation == Orientation.Horizontal);
            Rect rcChild = new Rect(finalSize);
            double previousChildSize = 0.0;
            var spacing = Spacing;

            // Sort the children by ZIndex
            var sorted = from child in Children
                         orderby child.ZIndex
                         select child;

            foreach (var child in sorted)
            {
                if (!child.IsVisible)
                {
                    continue;
                }

                if (fHorizontal)
                {
                    rcChild = rcChild.WithX(rcChild.X + previousChildSize);
                    previousChildSize = child.DesiredSize.Width;
                    rcChild = rcChild.WithWidth(previousChildSize);
                    rcChild = rcChild.WithHeight(Math.Max(finalSize.Height, child.DesiredSize.Height));
                    previousChildSize += spacing;
                }
                else
                {
                    rcChild = rcChild.WithY(rcChild.Y + previousChildSize);
                    previousChildSize = child.DesiredSize.Height;
                    rcChild = rcChild.WithHeight(previousChildSize);
                    rcChild = rcChild.WithWidth(Math.Max(finalSize.Width, child.DesiredSize.Width));
                    previousChildSize += spacing;
                }

                child.Arrange(rcChild);
            }

            return finalSize;
        }
    }
}

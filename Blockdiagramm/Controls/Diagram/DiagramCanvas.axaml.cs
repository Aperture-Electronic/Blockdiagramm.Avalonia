using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using Avalonia.Remote.Protocol.Input;
using Blockdiagramm.Controls.Diagram.Component;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Models;
using Blockdiagramm.Models.Diagram;
using Blockdiagramm.Renderer.Wiring;
using Blockdiagramm.ViewModels;
using Blockdiagramm.ViewModels.Diagram.Component;
using Blockdiagramm.ViewModels.Diagram.Wire;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Blockdiagramm.Controls.Diagram
{
    public partial class DiagramCanvas : UserControl
    {
        private double canvasMaxHeight = 0;
        private double canvasMaxWidth = 0;

        public DiagramCanvas()
        {
            InitializeComponent();
        }

        public override void Render(DrawingContext context)
        {
            base.Render(context);

            canvasMaxWidth = (Bounds.Width > canvasMaxWidth) ?
                Bounds.Width : canvasMaxWidth;
            canvasMaxHeight = (Bounds.Height > canvasMaxHeight) ?
                Bounds.Height : canvasMaxHeight;

            if (DataContext is DiagramModel model)
            {
                model.Grid.Width = canvasMaxWidth;
                model.Grid.Height = canvasMaxHeight;
            }
        }

        private void OnCanvasPointerPressed(object sender, PointerPressedEventArgs e)
        {
            PointerPoint currentPoint = e.GetCurrentPoint(itemCanvas);

            if (DataContext is not DiagramModel model)
            {
                return;
            }

            if (currentPoint.Properties.IsLeftButtonPressed)
            {
                if (model.WiringManager.IsWiring)
                {
                    model.WiringManager.CancelWiring();
                }
            }
        }

        private void OnCanvasPointerMoved(object sender, PointerEventArgs e)
        {
            if (DataContext is not DiagramModel model)
            {
                return;
            }

            if (model.WiringManager.IsWiring)
            {
                // Get the position
                Point position = e.GetPosition(itemCanvas);
                model.WiringManager.GuideWiring(position);
            }
        }
    }
}

using Avalonia;
using Avalonia.Media;
using Blockdiagramm.Controls.Diagram.Background;
using Blockdiagramm.Controls.Diagram.Component;
using Blockdiagramm.Controls.Diagram.Wire;
using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring;
using Blockdiagramm.ViewModels.Diagram;
using Blockdiagramm.ViewModels.Diagram.Component;
using Blockdiagramm.ViewModels.Diagram.Wire;
using DynamicData;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Blockdiagramm.Models.Diagram
{
    public class DiagramModel : ReactiveObject
    {
        private string name;
        private readonly SourceList<IDiagramItem> items = new();
        private readonly ReadOnlyObservableCollection<IDiagramItem> observableItems;
        private readonly List<WireConnection> connections = new();
        private PortOnPart connectFrom;

        public string Name
        {
            get => name;
            set => this.RaiseAndSetIfChanged(ref name, value);
        }

        public ReadOnlyObservableCollection<IDiagramItem> Items => observableItems;

        /// <summary>
        /// Wiring manager of the diagram
        /// </summary>
        public WiringManager WiringManager { get; }

        public DiagramGrid Grid
        {
            get
            {
                IDiagramItem? gridItem = Items.FirstOrDefault(x => x.DiagramType == DiagramItemType.Grid);
                return gridItem is not DiagramGrid grid ? throw new Exception("The diagram has not grid.") : grid;
            }
        }

        public DiagramModel(string name)
        {
            this.name = name;

            // Bind the list to items
            items.Connect().Bind(out observableItems).Subscribe();

            // Add the grid to the canvas
            DiagramGrid grid = new();
            items.Add(grid);

            // Create the wiring manager
            WiringManager = new WiringManager(this);
        }

        public void AddComponent(ComponentPartInstanceModel model)
        {
            ComponentPart part = new()
            {
                DataContext = model
            };

            part.ComponentMoved += OnComponentMoved;
            part.PortPressed += OnPortPressed;

            items.Add(part);
        }

        public void AddWire(VertexWire wire) => items.Add(wire);

        public void RemoveWire(VertexWire wire) => items.Remove(wire);

        #region Events
        private void OnComponentMoved(object? sender, ComponentMovedEventArgs e)
        {
            if (sender is ComponentPart part)
            {
                // Get the translation delta of the part
                Point delta = e.NewPosition - e.PreviousPosition;

                // Query all connections for this part
                var connsRelatedToPart = from conn in connections
                                         where conn.IsRelatedTo(part)
                                         select conn;

                // Set these connections to temp
                // part.IsObstacleValid = false;
                connsRelatedToPart.Apply(conn => conn.WireModel.WireStatus = WireStatus.Temporary);

                // Re-route these connections
                bool firstConnect = true;
                foreach (WireConnection conn in connsRelatedToPart)
                {
                    conn.RerouteWire(WiringManager, firstConnect);

                    //if (!conn.TranslateWire(WiringManager, delta, updateObstacles: firstConnect))
                    //{
                    //    // Need re-route
                    //    part.IsObstacleValid = true;
                    //    conn.RerouteWire(WiringManager);
                    //    part.IsObstacleValid = false;

                    // After reroute, we need add new wire to the obstacles list
                    WiringManager.AddWireAsObstacle(conn.Wire);

                    // Set re-routed connection to normal
                    conn.WireModel.WireStatus = WireStatus.Normal;
                    //}

                    firstConnect = false;
                }

                // Set these connections to normal
                connsRelatedToPart.Apply(conn => conn.WireModel.WireStatus = WireStatus.Normal);
                // part.IsObstacleValid = true;
            }
        }

        private void OnPortPressed(object? sender, ComponentPortPressedEventArgs e)
        {
            if (sender is ComponentPart part)
            {
                if (part.RenderTransform is TranslateTransform partTransform)
                {
                    Point portPoint = part.GetPortPosition(e.Port);

                    if (WiringManager.IsWiring)
                    {
                        var wire = WiringManager.CompleteWiring(portPoint, e.PortModel.Direction);

                        // Make new connection
                        PortOnPart connectTo = new(part, e.Port);
                        WireConnection connection = new(connectFrom, connectTo, wire);
                        connections.Add(connection);
                    }
                    else
                    {
                        connectFrom = new(part, e.Port);
                        WiringManager.StartWiring(portPoint, e.PortModel.Direction);
                    }
                }
            }
        }
        #endregion
    }
}

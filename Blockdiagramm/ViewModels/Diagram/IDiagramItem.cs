
namespace Blockdiagramm.ViewModels.Diagram
{
    /// <summary>
    /// Interface for all items in the diagram. Includes the grid, components, wires, etc.
    /// </summary>
    public interface IDiagramItem
    {
        DiagramItemType DiagramType { get; }
    }
}

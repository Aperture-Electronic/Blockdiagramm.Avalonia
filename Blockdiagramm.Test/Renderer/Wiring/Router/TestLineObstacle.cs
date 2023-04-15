using Blockdiagramm.Extensions;
using Blockdiagramm.Renderer.Wiring.Router;

namespace Blockdiagramm.Test.Renderer.Wiring.Router
{
    internal class TestLineObstacle : ILineObstacle
    {
        public bool IsObstacleValid { get; set; }
        public IEnumerable<OrthogonalSegment> Segments => segments;

        private readonly List<OrthogonalSegment> segments = new();

        public TestLineObstacle(OrthogonalSegment segment) => segments.Add(segment);

        public TestLineObstacle(IEnumerable<OrthogonalSegment> segments) => this.segments.AddRange(segments);
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Renderer.Wiring.Router
{
    public class RouterConfiguration
    {
        /// <summary>
        /// Minimum step of router, the router will find neighbors under this step
        /// </summary>
        public double Step { get; set; } = 10;

        /// <summary>
        /// The horizontal fanout length when connect to a port
        /// </summary>
        public double PortFanout { get; set; } = 10;

        /// <summary>
        /// Deflate the bound box to hit, avoiding unable to reach end-point
        /// </summary>
        public double BoundBoxDeflate { get; set; } = 1;

        /// <summary>
        /// Maximum iteration count of router, when over, an exception will throw
        /// </summary>
        public int MaximumIteration { get; set; } = 100000;

        /// <summary>
        /// Threshold to determine collinear/intersecting
        /// </summary>
        public double Threshold { get; set; } = double.Epsilon;

        /// <summary>
        /// Additional cost when the route cross an other vertex line
        /// </summary>
        public double CrossCost { get; set; } = 10;

        /// <summary>
        /// Additional cost when the route turn left/right
        /// </summary>
        public double TurnCost { get; set; } = 20;
    }
}

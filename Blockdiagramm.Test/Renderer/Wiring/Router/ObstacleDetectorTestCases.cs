using Blockdiagramm.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Test.Renderer.Wiring.Router
{
    public static class ObstacleDetectorTestCases
    {
        public readonly static List<ObstacleDetectorTestCase> Cases = new()
        {
            new((0.0, 0.0).ToPoint(), (1.0, 0.0).ToPoint(), new()
            {
                new((0.0, 0.0).ToPoint(), (0.0, 1.0).ToPoint()) 
            }),
            new((0.0, 0.0).ToPoint(), (1.0, 0.0).ToPoint(), new()
            {
                new((0.5, 0.0).ToPoint(), (0.5, 1.0).ToPoint())
            }),
            new((0.0, 0.0).ToPoint(), (1.0, 0.0).ToPoint(), new()
            {
                new((0.0, 0.5).ToPoint(), (0.0, 1.0).ToPoint())
            })
        };
    }
}

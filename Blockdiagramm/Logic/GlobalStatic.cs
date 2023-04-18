using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public static class GlobalStatic
    {
        public static Project Project { get; }

        static GlobalStatic()
        {
            Project = new();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Logic
{
    public enum SourceFileType
    {
        Auto = 0x00,
        SystemVerilogSource = 0x01,
        VerilogSource = 0x0100,
        VHDLSource = 0x010000,
        SystemVerilogHeader = 0x02
    }
}

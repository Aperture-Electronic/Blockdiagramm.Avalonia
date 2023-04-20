using Avalonia.Media;
using Blockdiagramm.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class SourceFileTypeExtension
    {
        public static string Abbreviate(this SourceFileType fileType) => fileType switch
        {
            SourceFileType.Auto => "AUTO",
            SourceFileType.SystemVerilogSource => "SV",
            SourceFileType.VerilogSource => "V",
            SourceFileType.VHDLSource => "VHDL",
            SourceFileType.SystemVerilogHeader => "SVH",
            _ => "N/A"
        };

        public static IBrush ThemeColor(this SourceFileType fileType) => fileType switch
        {
            SourceFileType.Auto => new SolidColorBrush(Colors.DarkGreen),
            SourceFileType.SystemVerilogSource => new SolidColorBrush(0xFF1976D2),
            SourceFileType.VerilogSource => new SolidColorBrush(Colors.DarkSlateBlue),
            SourceFileType.VHDLSource => new SolidColorBrush(Colors.DarkGoldenrod),
            SourceFileType.SystemVerilogHeader => new SolidColorBrush(Colors.DarkKhaki),
            _ => new SolidColorBrush(Colors.DarkGray)
        };
    }
}

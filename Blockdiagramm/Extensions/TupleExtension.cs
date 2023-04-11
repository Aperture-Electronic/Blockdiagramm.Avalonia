using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class TupleExtension
    {
        public static Avalonia.Point ToPoint(this (double x, double y) p)
            => new(p.x, p.y);
    }
}

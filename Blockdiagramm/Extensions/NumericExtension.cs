using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static class NumericExtension
    {
        public static double NaNAsZero(this double value) => double.IsNaN(value) ? 0 : value;

        public static double Square(this double value) => value * value;    
    }
}

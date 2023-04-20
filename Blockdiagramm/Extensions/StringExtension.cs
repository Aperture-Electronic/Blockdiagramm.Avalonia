using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public static partial class StringExtension
    {
        [GeneratedRegex("(?<!^)(?=[A-Z])")]
        private static partial Regex CapitalLetterRegex();

        public static string SplitByCapital(this string input, char separator = ' ')
        {
            string[] splited = CapitalLetterRegex().Split(input);
            return string.Join(separator, splited);
        }
    }
}

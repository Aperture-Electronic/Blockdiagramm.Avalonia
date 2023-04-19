using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public readonly struct EnumWithDisplayName<T> where T : Enum
    {
        public T Value { get; }
        public string DisplayName { get; }

        public override string ToString() => DisplayName;

        public EnumWithDisplayName(T value, string displayName)
        {
            Value = value;
            DisplayName = displayName;
        }
    }
}

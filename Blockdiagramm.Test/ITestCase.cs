using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Test
{
    public interface ITestCase<T>
    {
        public T CorrectResult { get; }
    }
}

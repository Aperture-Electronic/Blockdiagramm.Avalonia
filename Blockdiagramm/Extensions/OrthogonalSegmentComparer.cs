using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blockdiagramm.Extensions
{
    public class OrthogonalSegmentComparer : IComparer<OrthogonalSegment>
    {
        public int Compare(OrthogonalSegment x, OrthogonalSegment y) => x.CompareTo(y);
    }
}

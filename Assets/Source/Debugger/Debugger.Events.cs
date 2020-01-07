using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta
{
    public static partial class Debugger
    {
        public delegate void DrawChunkBordersChangedDelegate(bool newState);
        public static event DrawChunkBordersChangedDelegate DrawChunkBordersChanged;
    }
}

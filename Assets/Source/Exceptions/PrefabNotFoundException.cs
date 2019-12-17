using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exceptions
{
    public class PrefabNotFoundException : Exception
    {
        public PrefabNotFoundException() {}

        public PrefabNotFoundException(string index) : base($"Prefab with index {index} not found.") { }
    }
}


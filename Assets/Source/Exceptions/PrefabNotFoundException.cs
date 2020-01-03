using System;

namespace Zetta.Exceptions
{
    public class PrefabNotFoundException : Exception
    {
        public PrefabNotFoundException()
        {
        }

        public PrefabNotFoundException(string index)
            : base($"Prefab with index {index} not found.")
        {
        }
    }
}
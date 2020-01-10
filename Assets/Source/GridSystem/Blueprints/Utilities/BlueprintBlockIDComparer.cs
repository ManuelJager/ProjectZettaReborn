using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.GridSystem.Blueprints.Utilities
{
    public class BlueprintBlockIDComparer : IEqualityComparer<BlueprintBlock>
    {
        public bool Equals(BlueprintBlock x, BlueprintBlock y)
        {
            return x.BlockTypeID == y.BlockTypeID;
        }

        public int GetHashCode(BlueprintBlock obj)
        {
            return obj.BlockTypeID.GetHashCode();
        }
    }
}


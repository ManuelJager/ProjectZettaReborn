using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;
using System.Linq;

namespace Zetta.GridSystem.Blueprints
{
    // values asociated with block id
    // used as cache
    public struct BlueprintRuntimeReadonlyValues
    {
        public readonly Vector2 size;
        public readonly float rating;

        internal BlueprintRuntimeReadonlyValues(Vector2 size, float rating)
        {
            this.size = size;
            this.rating = rating;
        }
    }

    public static class RuntimeValues
    {
        private static readonly Dictionary<string, BlueprintRuntimeReadonlyValues> 
            values = new Dictionary<string, BlueprintRuntimeReadonlyValues>();

        public static BlueprintRuntimeReadonlyValues Get(string id)
        {
            return values[id];
        }

        public static void Initialize()
        {
            var IDPrefabDictionary = GameManager.PrefabProviderInstance.Instance.AsDictionary;
            foreach (var item in IDPrefabDictionary)
            {
                var gridBlockBase = (GridBlockBase)item.Value.GetComponent(typeof(GridBlockBase));
                var size = gridBlockBase.Size;
                var rating = 1f;
                values[item.Key] = new BlueprintRuntimeReadonlyValues(size, rating);
            }
        }
    }
}
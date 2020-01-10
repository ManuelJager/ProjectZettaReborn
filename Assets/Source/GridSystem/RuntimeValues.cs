using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blocks;

namespace Zetta.GridSystem
{
    public static class RuntimeValues
    {
        public static readonly Dictionary<string, Vector2> sizeValue = new Dictionary<string, Vector2>();

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            var IDPrefabDictionary = GameManager.PrefabProviderInstance.Instance.AsDictionary;

            foreach (var item in IDPrefabDictionary)
            {
                var gridBlockBase = (GridBlockBase)item.Value.GetComponent(typeof(GridBlockBase));
                sizeValue[item.Key] = gridBlockBase.Size;
            }
        }
    }
}
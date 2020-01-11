using System.Collections.Generic;
using UnityEngine;
using Zetta.Exceptions;
using Zetta.Generics;

namespace Zetta
{
    public partial class GameManager
    {
        [System.Obsolete("Use BlockPrefabProvide")]
        public static class PrefabProvider
        {
            public static GameObject GetPrefab(string index)
            {
                try
                {
                    return PrefabProviderInstance.Instance[index];
                }
                catch (KeyNotFoundException)
                {
                    throw new PrefabNotFoundException(index);
                }
            }
        }

        [System.Serializable]
        [System.Obsolete("Use BlockPrefabProvide")]
        public class PrefabProviderInstance : SerializableDictionary<string, GameObject>
        {
            public static PrefabProviderInstance Instance;

            public PrefabProviderInstance()
            {
                Instance = this;
            }
        }
    }
}
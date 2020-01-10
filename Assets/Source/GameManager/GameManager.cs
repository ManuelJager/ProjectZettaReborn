using UnityEngine;
using Zetta.GridSystem.Blueprints;
using Zetta.GridSystem.Blueprints.Thumbnails;
using Zetta.Generics;

namespace Zetta
{
    public partial class GameManager : AutoInstanceMonoBehaviour<GameManager>
    {
        public PrefabProviderInstance prefabProvider = new PrefabProviderInstance();

        [System.NonSerialized]
        public BlueprintInstantiator bpInstantiator;


        public new void Awake()
        {
            base.Awake();
            // Create an instance of the blueprint instantiator
            bpInstantiator = GetComponent<BlueprintInstantiator>();

        }

        private void InitializeGridSystem()
        {

        }
    }
}
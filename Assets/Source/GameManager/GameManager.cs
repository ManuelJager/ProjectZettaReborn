using Zetta.Generics;
using Zetta.GridSystem.Blueprints;

namespace Zetta
{
    public partial class GameManager : AutoInstanceMonoBehaviour<GameManager>
    {
        [System.Obsolete("Use BlockPrefabProvide")]
        public PrefabProviderInstance prefabProvider = 
           new PrefabProviderInstance();

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
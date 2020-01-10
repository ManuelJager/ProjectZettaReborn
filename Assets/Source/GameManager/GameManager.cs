using UnityEngine;
using Zetta.GridSystem.Blueprints;

namespace Zetta
{
    public partial class GameManager : MonoBehaviour
    {
        public static GameManager Instance;
        public PrefabProviderInstance prefabProvider = new PrefabProviderInstance();

        [System.NonSerialized]
        public BlueprintInstantiator bpInstantiator;

        public GameManager()
        {
            Instance = this;
        }

        public void Awake()
        {
            // Create an instance of the blueprint instantiator
            bpInstantiator = GetComponent<BlueprintInstantiator>();
            BlueprintManager.AddDefaultShipToLoadedBlueprints();
        }
    }
}
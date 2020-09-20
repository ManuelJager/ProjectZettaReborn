#pragma warning disable CS0649

using Zetta.Generics;
using Zetta.GridSystem.Blueprints.Thumbnails;
using Zetta.GridSystem.Blueprints;
using Zetta.GridSystem;
using Zetta.Audio.Atmosphere;
using UnityEngine;

namespace Zetta
{
    public partial class GameManager : AutoInstanceMonoBehaviour<GameManager>, IInitializeable, ISerializationCallbackReceiver
    {
        [SerializeField] private BlockPrefabProvider prefabProviderInstance;
        [SerializeField] private BlueprintManager blueprintManagerInstance;
        [SerializeField] private ThumbnailManager thumbnailManagerInstance;
        [SerializeField] private MusicManager musicManagerInstance;

        public static BlockPrefabProvider prefabProvider;
        public static BlueprintManager blueprintManager;
        public static ThumbnailManager thumbnailManager;
        public static MusicManager musicManager;

        public new void Awake()
        {
            base.Awake();
            Initialize();
        }

        public void Initialize()
        {
            // Initialize the block prefab provider
            // This involves reading from unity serialized data
            // Requires : None
            prefabProvider.Initialize();

            // Initialize the blueprint manager
            // This involves creating the model and main view model manager
            // and loading user blueprints from disk
            // Requires : None
            blueprintManager.Initialize();

            // Initialize the thumbnail manager
            // This involves loading blueprint thumbnails from disk identified by the 
            // blueprint hash
            // Requires : blueprint manager
            thumbnailManager.Initialize();

            // Initialize the music manager
            // This involves setting the current atmosphere
            // TODO : Remove setting atmosphere on initialization
            musicManager.Initialize();
        }

        public void OnAfterDeserialize()
        {
            // Set static references
            prefabProvider = prefabProviderInstance;
            blueprintManager = blueprintManagerInstance;
            thumbnailManager = thumbnailManagerInstance;
            musicManager = musicManagerInstance;
        }

        public void OnBeforeSerialize()
        {
            // Do nothing
        }
    }
}
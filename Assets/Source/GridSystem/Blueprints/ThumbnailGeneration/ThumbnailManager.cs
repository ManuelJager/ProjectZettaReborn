#pragma warning disable CS1998

using UnityEngine;
using UnityEngine.UI;
using Zetta.Generics;
using Zetta.FileSystem;

namespace Zetta.GridSystem.Blueprints.Thumbnails
{
    public partial class ThumbnailManager : AutoInstanceMonoBehaviour<ThumbnailManager>, IInitializeable
    {
        public Camera thumbnailCamera;
        public RenderTexture renderTexture;

        public SpriteCache spriteCache;

        public void Initialize()
        {
            renderTexture = new RenderTexture(new RenderTextureDescriptor(800, 800));
            thumbnailCamera.targetTexture = renderTexture;
            spriteCache = new SpriteCache();
        }

        public Sprite GetThumbnail(BlueprintModel blueprint, bool forceGet = false)
        {
            if (!forceGet)
            {
                if (spriteCache.ContainsKey(blueprint.GetHashCode()))
                {
                    return spriteCache[blueprint.GetHashCode()];
                }
            }

            var thumbnail = CreateThumbnail(blueprint);

            spriteCache[blueprint.GetHashCode()] = thumbnail;
            return thumbnail;
        }
    }
}
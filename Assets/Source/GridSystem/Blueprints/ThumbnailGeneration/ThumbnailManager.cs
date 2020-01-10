#pragma warning disable CS1998

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zetta.Generics;

namespace Zetta.GridSystem.Blueprints.Thumbnails
{
    public partial class ThumbnailManager : AutoInstanceMonoBehaviour<ThumbnailManager>
    {
        public Camera thumbnailCamera;
        public RenderTexture renderTexture;

        public SpriteCache spriteCache;

        public new void Awake()
        {
            base.Awake();
            renderTexture = new RenderTexture(new RenderTextureDescriptor(800, 800));
            thumbnailCamera.targetTexture = renderTexture;
        }

        public void Start()
        {
            spriteCache = new SpriteCache();
            spriteCache.LoadThumbnails();
            spriteCache.SaveCache();
        }

        public Sprite GetThumbnail(Blueprint blueprint, bool forceGet = false)
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

        public void SetThumbnail(Blueprint blueprint, Image image, bool forceGet = false)
        {
            var thumbnail = GetThumbnail(blueprint, forceGet);
            image.sprite = thumbnail;
        }
    }
}
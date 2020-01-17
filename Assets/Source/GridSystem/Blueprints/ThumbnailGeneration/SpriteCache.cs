#pragma warning disable CS4014

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zetta.Extensions;
using Zetta.Generics;
using Zetta.UI;
using Zetta.FileSystem;

namespace Zetta.GridSystem.Blueprints.Thumbnails
{
    public class SpriteCache : Dictionary<int, Sprite>, ISaveable
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 800;

        public event LoadedDelegate Loaded;
        public event SavedDelegate Saved;

        public SpriteCache()
        {
            Load(SpecialFolder.BlueprintThumbnails.GetPath());
        }

        public bool HasLoaded { get; } = false;

        public new Sprite this[int index]
        {
            get => base[index];
            set
            {
                base[index] = value;
                Save(SpecialFolder.BlueprintThumbnails.GetPath());
            }
        }

        public void Load(string path)
        {
            var fileNameNotValid = false;
            var files = Directory.GetFiles(SpecialFolder.BlueprintThumbnails.GetPath(), "*.png");
            for (int i = 0; i < files.Length; i++)
            {
                var texture = DirectoryExtensions.LoadPNG(files[i], WIDTH, HEIGHT);
                int hashKey;
                var name = Path.GetFileNameWithoutExtension(files[i]);
                var valid = int.TryParse(name, out hashKey);

                if (valid)
                {
                    base[hashKey] = Sprite.Create(
                        texture,
                        new Rect(0f, 0f, 800, 800),
                        new Vector2(0.5f, 0.5f));
                }
                else
                {
                    Debug.LogError($"file name not valid");
                }
            }

            if (fileNameNotValid)
            {
                NoticeManager.Instance.Prompt("Incorrect files found in thumbnails folder. Stay the fuck away :)");
            }

            Loaded?.Invoke();
        }

        public void Save(string path)
        {
            if (Keys.Count == 0)
            {
                return;
            }

            var fileNameNotValid = false;
            var files = Directory.GetFiles(path, "*.png");
            List<int> alreadyWriteThumbnailHashes = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                int hashKey;
                var name = Path.GetFileNameWithoutExtension(files[i]);
                var valid = int.TryParse(name, out hashKey);

                if (valid)
                {
                    alreadyWriteThumbnailHashes.Add(hashKey);
                }
                else
                {
                    fileNameNotValid = true;
                }
            }

            if (fileNameNotValid)
            {
                NoticeManager.Instance.Prompt("Incorrect files found in thumbnails folder. Stay the fuck away :)");
            }

            var setDifference = Keys.Except(alreadyWriteThumbnailHashes).ToList();

            for (int i = 0; i < setDifference.Count; i++)
            {
                var bytes = base[setDifference[i]].texture.EncodeToPNG();
                var imgPath = Path.Combine(path, $"{setDifference[i]}.png");
                File.WriteAllBytes(imgPath, bytes);
            }

            Saved.Invoke();
        }
    }
}
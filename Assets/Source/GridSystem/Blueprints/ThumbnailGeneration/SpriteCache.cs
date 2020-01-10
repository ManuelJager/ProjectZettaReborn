#pragma warning disable CS4014
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using Zetta.Extensions;
using Zetta.GridSystem.Blueprints;
using System.Linq;
using Zetta.UI;

namespace Zetta.GridSystem.Blueprints.Thumbnails
{
    public class SpriteCache : Dictionary<int, Sprite>
    {
        private const int WIDTH = 800;
        private const int HEIGHT = 800;
        private static string savePath = "";

        /// <summary>
        /// Dont call in constructor
        /// </summary>
        public SpriteCache()
            : base(OpenCache())
        {
        }

        private static string SavePath
        {
            get
            {
                if (savePath == "")
                {
                    var path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                    path = Path.Combine(path, "Zetta", "Thumbnails");
                    Directory.CreateDirectory(path);
                    savePath = path;
                }
                return savePath;
            }
        }

        public static Dictionary<int, Sprite> OpenCache()
        {
            var cache = new Dictionary<int, Sprite>();
            var fileNameNotValid = false;
            var files = Directory.GetFiles(SavePath, "*.png");
            for (int i = 0; i < files.Length; i++)
            {
                var texture = DirectoryExtensions.LoadPNG(files[i], WIDTH, HEIGHT);
                int hashKey;
                var name = Path.GetFileNameWithoutExtension(files[i]);
                var valid = int.TryParse(name, out hashKey);

                if (valid)
                {
                    cache[hashKey] = Sprite.Create(
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

            return cache;
        }

        public void SaveCache()
        {
            if (Keys.Count == 0)
            {
                return;
            }

            var fileNameNotValid = false;
            var files = Directory.GetFiles(SavePath, "*.png");
            List<int> alreadyWriteThumbnailHashes = new List<int>();
            for (int i = 0; i < files.Length; i++)
            {
                int hashKey;
                var name = Path.GetFileNameWithoutExtension(files[i]);
                var valid = int.TryParse(name, out hashKey);

                if (valid)
                {
                    if (BlueprintManager.loadedBlueprints.Hashes.Contains(hashKey))
                    {
                        alreadyWriteThumbnailHashes.Add(hashKey);
                    }
                    else
                    {
                        File.Delete(SavePath);
                    }
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
                var path = Path.Combine(SavePath, $"{setDifference[i]}.png");
                File.WriteAllBytes(path, bytes);
            }
        }

        public void LoadThumbnails()
        {
            foreach (var item in BlueprintManager.loadedBlueprints)
            {
                base[item.GetHashCode()] = ThumbnailManager.Instance.GetThumbnail(item);
            }
        }
    }
}
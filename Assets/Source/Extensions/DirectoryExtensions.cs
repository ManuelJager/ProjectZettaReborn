using System.IO;
using UnityEngine;

namespace Zetta.Extensions
{
    public static class DirectoryExtensions
    {
        public static Texture2D LoadPNG(string filePath)
        {
            return LoadPNG(filePath, 200, 200);
        }

        public static Texture2D LoadPNG(string filePath, int width, int height)
        {
            Texture2D tex = null;
            byte[] fileData;

            if (File.Exists(filePath))
            {
                fileData = File.ReadAllBytes(filePath);
                tex = new Texture2D(width, height);
                tex.LoadImage(fileData);
            }
            return tex;
        }
    }
}
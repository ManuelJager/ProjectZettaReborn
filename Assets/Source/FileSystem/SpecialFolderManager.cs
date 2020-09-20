using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

namespace Zetta.FileSystem
{
    public enum SpecialFolder
    {
        BlueprintThumbnails
    }

    public enum SpecialFile
    {
        PlayerBlueprintCollection
    }

    public static class ZettaPath
    {
        private static Dictionary<SpecialFolder, string> folderPathCache = 
                   new Dictionary<SpecialFolder, string>();

        private static Dictionary<SpecialFile, string> filePathCache =
                   new Dictionary<SpecialFile, string>();

        

        private static string appData = "";
        private static string AppData
        {
            get
            {
                if (appData == "")
                {
                    appData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                }
                return appData;
            }
        }

        public static string GetPath(this SpecialFolder specialFolder)
        {
            try
            {
                return folderPathCache[specialFolder];
            }
            catch
            {
                folderPathCache[specialFolder] = GenerateFolderPath(specialFolder);
                return folderPathCache[specialFolder];
            }
        }

        public static string GetPath(this SpecialFile specialFile)
        {
            try
            {
                return filePathCache[specialFile];
            }
            catch
            {
                filePathCache[specialFile] = GenerateFilePath(specialFile);
                return filePathCache[specialFile];
            }
        }

        private static string GenerateFolderPath(SpecialFolder specialFolder)
        {
            switch (specialFolder)
            {
                case SpecialFolder.BlueprintThumbnails:
                    var path = Path.Combine(AppData, "Zetta", "Thumbnails");
                    Directory.CreateDirectory(path);
                    return path;
                default:
                    return "";
            }
        }

        private static string GenerateFilePath(SpecialFile specialFolder)
        {
            switch (specialFolder)
            {
                case SpecialFile.PlayerBlueprintCollection:
                    var path = Path.Combine(AppData, "Zetta", "loadedblueprints.zetta");
                    return path;
                default:
                    return "";
            }
        }

    }
}

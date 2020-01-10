using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zetta.Exceptions;
using Zetta.GridSystem.Blueprints.Thumbnails;

namespace Zetta.GridSystem.Blueprints
{
    public static partial class BlueprintManager
    {
        public class LoadedBlueprints : List<Blueprint>
        {
            private static readonly string savePath;
            private List<int> hashes = new List<int>();

            public delegate void BlueprintAddedDelegate(Blueprint blueprint);
            public delegate void BlueprintRemovedDelegate(Blueprint blueprint);

            public event BlueprintAddedDelegate BlueprintAdded;
            public event BlueprintRemovedDelegate BlueprintRemoved;

            static LoadedBlueprints()
            {
                var destinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                savePath = Path.Combine(destinationFolder, "loadedblueprints.zetta");
            }

            public LoadedBlueprints()
                : base(SerializeBlueprints())
            {
                RuntimeValues.Initialize();
            }

            // save blueprints to file
            ~LoadedBlueprints()
            {
                DeserializeBlueprints(this);
            }

            public List<int> Hashes
            {
                get
                {
                    return hashes;
                }
            }

            public static List<Blueprint> SerializeBlueprints()
            {
                try
                {
                    var json = File.ReadAllText(savePath);
                    var unverifiedBlueprints = JsonConvert.DeserializeObject<List<Blueprint>>(json);
                    // Return a list of blueprints that are valid and unique
                    return unverifiedBlueprints.Distinct().Where(x => x.IsValid).ToList();
                }
                catch (Exception e)
                {
                    Debug.LogWarning(e);
                    // if error, return an empty list
                    return new List<Blueprint>();
                }
            }

            public static void DeserializeBlueprints(LoadedBlueprints blueprints)
            {
                var json = JsonConvert.SerializeObject(blueprints, Formatting.Indented);

                using (var sr = File.CreateText(savePath))
                {
                    sr.WriteLine(json);
                }
            }

            public Blueprint GetFirstWithName(string name)
            {
                return (from blueprint in this
                        where blueprint.Name == name
                        select blueprint)
                        .FirstOrDefault();
            }

            // override base functionality to use custom blueprint hashing methods
            private new bool Contains(Blueprint pBlueprint)
            {
                var hash = pBlueprint.GetHashCode();
                foreach (var blueprint in this)
                {
                    if (blueprint.GetHashCode() == hash)
                    {
                        return true;
                    }
                }
                return false;
            }

            // override base functionality to only add unique blueprints
            public new void Add(Blueprint blueprint)
            {
                if (!Contains(blueprint))
                {
                    base.Add(blueprint);
                    hashes.Add(blueprint.GetHashCode());
                    BlueprintAdded?.Invoke(blueprint);
                }
                else
                {
                    throw new DuplicateBlueprintException();
                }
                
            }
            
            public new void Remove(Blueprint blueprint)
            {
                hashes.Remove(blueprint.GetHashCode());
                base.Remove(blueprint);
                BlueprintRemoved?.Invoke(blueprint);
            }
        }

        public static LoadedBlueprints loadedBlueprints = new LoadedBlueprints();

        public static void AddDefaultShipToLoadedBlueprints()
        {
            try
            {
                loadedBlueprints.Add(Import(DEFAULT_BLUEPRINT));
            }
            catch (DuplicateBlueprintException)
            {
            }
            catch (Exception e)
            {
                Debug.LogError(e);
            }
        }
    }
}
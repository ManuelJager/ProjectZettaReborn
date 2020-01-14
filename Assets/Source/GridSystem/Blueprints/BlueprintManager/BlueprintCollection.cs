#pragma warning disable CS0067

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zetta.Exceptions;
using Zetta.Generics;
using Zetta.FileSystem;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintCollection : List<Blueprint>, ISaveable
    {
        public delegate void AddedDelegate(Blueprint blueprint);

        public delegate void RemovedDelegate(Blueprint blueprint);

        public delegate void LoadedDelegate();

        public static event AddedDelegate Added;

        public static event RemovedDelegate Removed;

        public static event LoadedDelegate Loaded;

        public BlueprintCollection()
        {
            Load(SpecialFile.PlayerBlueprintCollection.GetPath());
        }

        private BlueprintCollection(List<Blueprint> blueprints)
            : base(blueprints)
        {
        }

        public List<int> Hashes { get; private set; } = new List<int>();

        public void PerformBlueprintsLoaded()
        {
            Loaded?.Invoke();
            for (int i = 0; i < Count; i++)
            {
                Added?.Invoke(base[i]);
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

        /// <summary>
        /// Add
        /// </summary>
        /// <param name="blueprints"></param>
        private void AddManySafe(List<Blueprint> blueprints)
        {
            for (int i = 0; i < blueprints.Count; i++)
            {
                if (!Contains(blueprints[i]))
                {
                    base.Add(blueprints[i]);
                    Hashes.Add(blueprints[i].GetHashCode());
                }
                else
                {
                    throw new DuplicateBlueprintException();
                }
            }
            Save(SpecialFile.PlayerBlueprintCollection.GetPath());
        }

        // override base functionality to only add unique blueprints
        public new void Add(Blueprint blueprint)
        {
            if (!Contains(blueprint))
            {
                base.Add(blueprint);
                Hashes.Add(blueprint.GetHashCode());

                Added?.Invoke(blueprint);
                Save(SpecialFile.PlayerBlueprintCollection.GetPath());
            }
            else
            {
                throw new DuplicateBlueprintException();
            }
        }

        public void Load(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var unverifiedBlueprints = JsonConvert.DeserializeObject<List<Blueprint>>(json);

                // Return a list of blueprints that are valid and unique
                AddManySafe(unverifiedBlueprints.Distinct().Where(x => x.IsValid).ToList());
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
                AddManySafe(new List<Blueprint>());
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error on blueprint collection load:" + e);
                // if error, return an empty list
                AddManySafe(new List<Blueprint>());
            }
        }

        public void Save(string path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);

            using (var sr = File.CreateText(path))
            {
                sr.WriteLine(json);
            }
        }
    }
}
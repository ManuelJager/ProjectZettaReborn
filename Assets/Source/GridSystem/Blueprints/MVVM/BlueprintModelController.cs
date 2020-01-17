using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.Generics;
using System;
using Zetta.FileSystem;
using System.IO;
using Newtonsoft.Json;
using System.Linq;
using Zetta.Exceptions;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintModelController : ModelControllerBase<BlueprintModel>, ISaveable
    {
        public List<int> Hashes = new List<int>();

        public event LoadedDelegate Loaded;
        public event SavedDelegate Saved;

        public bool HasLoaded { get; private set; } = false;

        private string savePath;

        public BlueprintModelController(string path)
        {
            this.savePath = path;
        }

        public void Load()
        {
            Load(savePath);
        }

        public void Save()
        {
            Save(savePath);
        }

        public void Load(string path)
        {
            try
            {
                var json = File.ReadAllText(path);

                var unverifiedBlueprints = JsonConvert.DeserializeObject<List<BlueprintModel>>(json);
                var validBlueprints = unverifiedBlueprints.Distinct().Where(x => x.IsValid);

                // Return a list of blueprints that are valid and unique
                AddRange(validBlueprints);
                Loaded?.Invoke();
                HasLoaded = true;
            }
            catch (FileNotFoundException)
            {
                File.Create(path);
                Loaded?.Invoke();
                HasLoaded = true;
            }
            catch (Exception e)
            {
                Debug.LogWarning("Error on blueprint collection load:" + e);
            }
        }

        public void Save(string path)
        {
            var json = JsonConvert.SerializeObject(this, Formatting.Indented);
            try
            {
                if (!File.Exists(path))
                {
                    File.Create(path);
                }

                File.WriteAllText(path, json);
                Saved?.Invoke();
            }
            catch (Exception e)
            {
                Debug.LogError("Error on blueprint collection load:" + e);
            }
        }

        /// <summary>
        /// Add without having to handle the DuplicateBlueprintException
        /// </summary>
        /// <param name="blueprint"></param>
        public void AddSafe(BlueprintModel blueprint)
        {
            try
            {
                Add(blueprint);
            }
            catch { }
        }

        public new void AddRange(IEnumerable<BlueprintModel> blueprints)
        {
            foreach (var blueprint in blueprints)
            {
                Add(blueprint);
            }
        }

        /// <summary>
        /// Add blueprint to collection
        /// Throws a DuplicateBlueprintException if the blueprint already exists
        /// </summary>
        /// <param name="blueprint"></param>
        public new void Add(BlueprintModel blueprint)
        {
            if (Contains(blueprint))
            {
                throw new DuplicateBlueprintException();
            }
            else
            {
                base.Add(blueprint);
                Hashes.Add(blueprint.GetHashCode());
            }
        }

        public new void Remove(BlueprintModel blueprint)
        {
            if (Contains(blueprint))
            {
                base.Remove(blueprint);
                Hashes.Remove(blueprint.GetHashCode());
            }
            else
            {
                throw new Exception("Blueprint not found");
            }
        }

        public BlueprintModel GetFirstWithName(string name)
        {
            return (from blueprint in this
                    where blueprint.Name == name
                    select blueprint)
                    .FirstOrDefault();
        }
    }
}

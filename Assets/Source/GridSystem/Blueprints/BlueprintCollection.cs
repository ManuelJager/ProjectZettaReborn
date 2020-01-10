using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using Zetta.Exceptions;
using Zetta.GridSystem.Blueprints.Thumbnails;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintCollection : List<Blueprint>
    {
        public static readonly string savePath;
        private List<int> hashes = new List<int>();

        public delegate void BlueprintAddedDelegate(Blueprint blueprint);
        public delegate void BlueprintRemovedDelegate(Blueprint blueprint);
        public delegate void BlueprintsLoadedDelegate();

        public static event BlueprintAddedDelegate BlueprintAdded;
        public static event BlueprintRemovedDelegate BlueprintRemoved;
        public static event BlueprintsLoadedDelegate BlueprintsLoaded;


        static BlueprintCollection()
        {
            var destinationFolder = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            savePath = Path.Combine(destinationFolder, "loadedblueprints.zetta");
        }

        public BlueprintCollection(List<Blueprint> blueprints)
        {
            for (int i = 0; i < blueprints.Count; i++)
            {
                Add(blueprints[i], true);
            }
        }

        // Quick access to blueprint hashes
        public List<int> Hashes
        {
            get
            {
                return hashes;
            }
        }

        public void PerformBlueprintsLoaded()
        {
            BlueprintsLoaded?.Invoke();
            for (int i = 0; i < Count; i++)
            {
                BlueprintAdded?.Invoke(base[i]);
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
        public new void Add(Blueprint blueprint, bool nonLoadTimeMode = false)
        {
            if (!Contains(blueprint))
            {
                base.Add(blueprint);
                hashes.Add(blueprint.GetHashCode());

                if (nonLoadTimeMode) return;

                BlueprintAdded?.Invoke(blueprint);
                BlueprintManager.DeserializeBlueprints(this, savePath);
            }
            else
            {
                throw new DuplicateBlueprintException();
            }
        }
    }
}
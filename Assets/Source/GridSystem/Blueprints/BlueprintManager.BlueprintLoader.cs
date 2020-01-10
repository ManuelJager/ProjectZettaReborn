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
    public partial class BlueprintManager
    {
        public static BlueprintCollection SerializeBlueprints(string path)
        {
            try
            {
                var json = File.ReadAllText(path);
                var unverifiedBlueprints = JsonConvert.DeserializeObject<List<Blueprint>>(json);

                // Return a list of blueprints that are valid and unique
                return new BlueprintCollection(unverifiedBlueprints.Distinct().Where(x => x.IsValid).ToList());
            }
            catch (Exception e)
            {
                Debug.LogWarning(e);
                // if error, return an empty list
                return new BlueprintCollection(new List<Blueprint>());
            }
        }

        public static void DeserializeBlueprints(BlueprintCollection blueprints, string path)
        {
            var json = JsonConvert.SerializeObject(blueprints, Formatting.Indented);

            using (var sr = File.CreateText(path))
            {
                sr.WriteLine(json);
            }
        }

        public static void AddDefaultShipToLoadedBlueprints()
        {
            try
            {
                blueprints.Add(Import(DEFAULT_BLUEPRINT));
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
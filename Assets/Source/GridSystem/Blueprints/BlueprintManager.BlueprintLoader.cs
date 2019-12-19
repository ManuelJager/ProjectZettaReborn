using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Exceptions;
using GridSystem;
using Newtonsoft.Json;
using UnityEngine;

namespace Blueprints
{
    public static partial class BlueprintManager
    {
		public class LoadedBlueprints : List<Blueprint>
        {
            public Blueprint GetBlueprint(string name)
            {
                return (from blueprint in this
                        where blueprint.Name == name 
                        select blueprint)
                        .FirstOrDefault();
            }

            public bool Contains(string name)
            {
                foreach (var blueprint in this)
                {
                    if (blueprint.Name == name)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public static LoadedBlueprints loadedBlueprints = new LoadedBlueprints();

        [RuntimeInitializeOnLoadMethod]
        public static void AddDefaultShipToLoadedBlueprints()
        {
            loadedBlueprints.Add(Import(DEFAULT_BLUEPRINT)); 
        }
    }
}
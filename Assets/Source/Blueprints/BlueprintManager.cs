using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using UnityEngine;

namespace Blueprints
{
    class BlueprintManager : MonoBehaviour
    {

        /// <summary>
        /// Parses the current blueprint to a string represatation
        /// </summary>
        /// <param name="blueprint">The blueprint to export</param>
        /// <returns>The blueprint string</returns>
        public static string Export(Blueprint blueprint)
        {
            // Creates the data 
            var jsonData = new Dictionary<string, object>
            {
                { "Name", blueprint.Name },
                { "Blocks", blueprint.Blocks }
            };

            // Serializes the data
            string json = JsonConvert.SerializeObject(jsonData);

            return json;
        }

        /// <summary>
        /// Deserializes the given json to an instance of the class "Blueprint"
        /// </summary>
        /// <param name="json">The json to deserialize</param>
        /// <returns>The deserialized blueprint</returns>
        public static Blueprint Import(string json)
        {
            Blueprint blueprint = JsonConvert.DeserializeObject<Blueprint>(json);
            
            return blueprint;
        }
    }
}

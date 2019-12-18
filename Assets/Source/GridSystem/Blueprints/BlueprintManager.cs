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
    public static class BlueprintManager
    {
        // The default blueprint
        public static string DEFAULT_BLUEPRINT = "{\"Name\":\"Default Ship\",\"Blocks\":[{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-4.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-3.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-2.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":1.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":1.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":3.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":3.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":3.0,\"y\":2.0},\"Rotation\":0}]}";

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

        /// <summary>
        /// Counts the amount of occurences of the item, in the list
        /// </summary>
        /// <param name="list">The list to search through</param>
        /// <param name="valueToFind">The value to find</param>
        /// <returns>The amount of occurences in the list</returns>
        private static int CountOccurenceOfValue(List<Vector2> list, Vector2 valueToFind)
        {
            return ((from temp in list where temp.Equals(valueToFind) select temp).Count());
        }

        /// <summary>
        /// Finds the duplicate items in the given list
        /// </summary>
        /// <param name="list">The list to search trough for duplicates</param>
        /// <returns>The duplicate items in the list</returns>
        private static List<Vector2> FindDuplicates(List<Vector2> list)
        {
            var duplicates = new List<Vector2>();
            foreach(Vector2 item in list)
                if (CountOccurenceOfValue(list, item) > 1 && !duplicates.Contains(item))
                    duplicates.Add(item);

            return duplicates;
        }

        /// <summary>
        /// Checks if the given blueprint is a valid blueprint
        /// </summary>
        /// <param name="blueprint">The blueprint to validate</param>
        /// <returns>The invalid blueprint blocks</returns>
        public static List<Vector2> ValidateBlueprint(Blueprint blueprint)
        {
            var positions = new List<Vector2>();

            // Add all position blocks of the blueprint to the positions list
            foreach(BlueprintBlock blueprintBlock in blueprint.Blocks)
            {
                try
                {
                    // Get the prefab
                    GameObject prefab = PrefabProvider.GetPrefab(blueprintBlock.BlockTypeID);

                    // Get the blockbase script
                    GridBlockBase blockBase = (GridBlockBase)prefab.GetComponent(typeof(GridBlockBase));
                    Vector2[] currentPositions = blockBase.BlockPositions;
                    foreach (Vector2 vector in currentPositions)
                    {
                        // Add the position to the list
                        positions.Add(new Vector2(
                            blueprintBlock.VectorPosition.x + vector.x,
                            blueprintBlock.VectorPosition.y + vector.y
                        ));
                    }
                }
                catch (PrefabNotFoundException) { continue; }
            }

            // Returns the duplicates of the positions
            return FindDuplicates(positions);
        }

        private static Blueprint CreateTestBlueprint(int size)
        {

            List<BlueprintBlock> blocks = new List<BlueprintBlock>();
            blocks.Add(new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, 0)));

            for (int i = 1; i < size + 1; i++)
            {
                blocks.Add(new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, i)));
                blocks.Add(new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(i, 0)));
                blocks.Add(new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, -i)));
                blocks.Add(new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-i, 0)));
            }

            return new Blueprint("Test Ship", blocks);
        }

        private static Blueprint TestShip()
        {
            return new Blueprint("Default Ship", new List<BlueprintBlock>() {
                new BlueprintBlock("Zetta::MediumThruster", new Vector2(-3, 0)),

                new BlueprintBlock("Zetta::SmallThruster", new Vector2(-2, 2)),
                new BlueprintBlock("Zetta::SmallThruster", new Vector2(-2, 1)),
                new BlueprintBlock("Zetta::SmallThruster", new Vector2(-2, -1)),
                new BlueprintBlock("Zetta::SmallThruster", new Vector2(-2, -2)),

                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-1, 2)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-1, 1)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-1, 0)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-1, -1)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(-1, -2)),

                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, 1)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, 0)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(0, -1)),

                new BlueprintBlock("Zetta::SmallThruster", new Vector2(1, 2), 1),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(1, 1)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(1, -1)),
                new BlueprintBlock("Zetta::SmallThruster", new Vector2(1, -2), 3),

                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(2, 2)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(2, 1)),
                new BlueprintBlock("Zetta::Cockpit", new Vector2(2, 0)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(2, -1)),
                new BlueprintBlock("Zetta::LightArmorBlock", new Vector2(2, -2)),

                new BlueprintBlock("Zetta::SmallThruster", new Vector2(3, -2), 2),
                new BlueprintBlock("Zetta::SmallThruster", new Vector2(3, 2), 2),
            });
        }

        //TODO Remove this later when don't need it anymore
        [RuntimeInitializeOnLoadMethod]
        public static void createTestBlueprint()
        {
            Blueprint testBp = TestShip();

            DEFAULT_BLUEPRINT = Export(testBp);
            if (ValidateBlueprint(testBp).Count <= 0)
            {
                Debug.Log("Valid Test Ship");
            }
            else
            {
                Debug.Log("Invalid Test Ship");
            }
        }
    }
}

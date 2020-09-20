using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Generics;
using Zetta.FileSystem;
using System.IO;
using Newtonsoft.Json;
using Zetta.Attributes;
using Zetta.Exceptions;
using Zetta.GridSystem;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintManager : AutoInstanceMonoBehaviour<BlueprintManager>, IInitializeable
    {
        internal BlueprintModelController userBlueprintsModelController;
        internal BlueprintViewModelController userBlueprintsViewModelController;

        private static readonly string DEFAULT_BLUEPRINT = "{\"Name\":\"Default Ship\",\"Blocks\":[{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-4.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-3.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-2.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":-2.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":-1.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":0.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":1.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":1.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":1.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":-1.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":2.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":3.0,\"y\":-2.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::LightArmorBlock\",\"Position\":{\"x\":3.0,\"y\":0.0},\"Rotation\":0},{\"BlockTypeID\":\"Zetta::SmallThruster\",\"Position\":{\"x\":3.0,\"y\":2.0},\"Rotation\":0}]}";

        public void Initialize()
        {
            // Initialize on constructor
            userBlueprintsModelController = new BlueprintModelController(SpecialFile.PlayerBlueprintCollection.GetPath());
            userBlueprintsViewModelController = new BlueprintViewModelController();
            userBlueprintsModelController.Connect(userBlueprintsViewModelController);

            // Load on start so scripts using the
            userBlueprintsModelController.Load();
            userBlueprintsModelController.AddSafe(Import(DEFAULT_BLUEPRINT));
        }

        /// <summary>
        /// Parses the current blueprint to a string represatation
        /// </summary>
        /// <param name="blueprint">The blueprint to export</param>
        /// <returns>The blueprint string</returns>
        public static string Export(BlueprintModel blueprint)
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
        public static BlueprintModel Import(string json)
        {
            var blueprint = JsonConvert.DeserializeObject<BlueprintModel>(json);

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
            return (from temp in list where temp.Equals(valueToFind) select temp).Count();
        }

        /// <summary>
        /// Finds the duplicate items in the given list
        /// </summary>
        /// <param name="list">The list to search trough for duplicates</param>
        /// <returns>The duplicate items in the list</returns>
        private static List<Vector2> FindDuplicates(List<Vector2> list)
        {
            var duplicates = new List<Vector2>();
            foreach (Vector2 item in list)
                if (CountOccurenceOfValue(list, item) > 1 && !duplicates.Contains(item))
                    duplicates.Add(item);

            return duplicates;
        }

        /// <summary>
        /// Checks if the given blueprint is a valid blueprint
        /// </summary>
        /// <param name="blueprint">The blueprint to validate</param>
        /// <returns>The invalid blueprint blocks</returns>
        [UglyCode]
        public static List<Vector2> ValidateBlueprint(BlueprintModel blueprint)
        {
            var positions = new List<Vector2>();

            // Add all position blocks of the blueprint to the positions list
            foreach (BlueprintBlock blueprintBlock in blueprint.Blocks)
            {
                try
                {
                    var blockBase = BlockPrefabProvider.Instance.GetGridBlockBase(blueprintBlock.BlockTypeID);

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

        private static BlueprintModel CreateTestBlueprint(int size)
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

            return new BlueprintModel("Test Ship", blocks);
        }

        private static BlueprintModel TestShip()
        {
            return new BlueprintModel("Default Ship", new List<BlueprintBlock>() {
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
    }
}

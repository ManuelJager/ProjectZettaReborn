#pragma warning disable CS4014

using UnityEngine;
using Zetta.Controllers;
using Zetta.GridSystem;
using Zetta.GridSystem.Blueprints;
using Zetta.UI;

namespace Zetta
{
    public static partial class Debugger
    {
        /// <summary>
        /// Instantiates a ship by the given blueprint name
        /// </summary>
        /// <param name="name">The blueprint name</param>
        public static void SpawnShipByName(string name)
        {
            var blueprint = BlueprintManager.Instance.userBlueprintsModelController.GetFirstWithName(name);
            if (blueprint != default)
            {
                Debug.Assert(PlayerController.Instance.Ship != null);
                Debug.Assert(PlayerController.Instance.Ship != default);
                Debug.Assert(blueprint != null);

                var ship = Ship.InstantiateShip(blueprint);

                Debug.Assert(ship != null);

                PlayerController.Instance.Ship = ship;
            }
            else
            {
                NoticeManager.Instance.Prompt($"Blueprint \"{name}\" not found");
                Debug.LogWarning($"Blueprint \"{name}\" not found");
            }
        }
    }
}
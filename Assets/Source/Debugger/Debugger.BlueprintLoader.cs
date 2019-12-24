using UnityEngine;
using System.Collections;
using GridSystem;

public static partial class Debugger
{
    public static void SpawnShipByName(string name)
    {
        var blueprint = Blueprints.BlueprintManager.loadedBlueprints.GetFirstWithName(name);
        if (blueprint != default)
        {
            PlayerController.Instance.Ship = Ship.InstantiateShip(blueprint);
        }
        else
        {
            Debug.LogWarning($"Blueprint {name} not found");
        }        
    }
}

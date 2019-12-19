using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using Extensions;
using Blueprints;

/// <summary>
/// Takes it input from global events from <see cref="InputManager"/>
/// </summary>
public class PlayerController : MonoBehaviour
{

    public void Start()
    {
        EntityController.Instance.InstantiatePlayer(
            BlueprintManager.createTestBlueprint()
            );

        EntityController.Instance.InstantiateShip(
            BlueprintManager.createTestBlueprint(),
            new Vector2(5, 5)
            );
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;

/// <summary>
/// Takes it input from global events from <see cref="InputManager"/>
/// </summary>
public class PlayerController
{
    public Ship ship;
    public PlayerController()
    {
        InputManager.InputAxis += OnAxis;
    }

    public void OnAxis(Vector2 input)
    {
        var inputRotation = (Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg + 360f) % 360f;
        MonoBehaviour.print(inputRotation);
    }
}

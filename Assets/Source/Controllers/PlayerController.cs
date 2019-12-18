using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GridSystem;
using Extensions;

/// <summary>
/// Takes it input from global events from <see cref="InputManager"/>
/// </summary>
public class PlayerController : MonoBehaviour
{
    [System.NonSerialized] public Ship ship;

    private Quaternion q;
    private Camera orthographicCamera;
    public void Awake()
    {
        InputManager.InputAxis += OnAxis;
    }

    public void Start()
    {
        ship = Ship.InstantiateShip();
        orthographicCamera = CameraExtensions.GetMainOrthgraphicCamera();
        orthographicCamera.enabled = false;
    }

    void Update()
    {
        q = GridUtilities.GetMouseWorldPos(ship.transform, orthographicCamera);
        ship.transform.rotation = GridUtilities.MouseLookAtRotation(ship.transform, 100);
    }

    /// <summary>
    /// Ship input
    /// </summary>
    /// <param name="input"></param>
    public void OnAxis(Vector2 input)
    {
        var inputRotation = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        inputRotation += q.eulerAngles.z + 270f;
        inputRotation %= 360f;
        input = GridUtilities.DegreeToVector2(inputRotation) * 2;
        ship.rb2d.AddForce(input);
    }
}

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
    private Ship ship;
    public Ship Ship
    {
        get => ship;
        set
        {
            Enabled = value != null;
            ship = value;
        }
    }
    public bool Enabled
    {
        get => enabled;
        set
        {
            gameObject.SetActive(value);
            UIManager.Instance.GameplayLayerActiveState = value;
        }
    }

    public static PlayerController Instance;

    private Quaternion q;
    private Camera orthographicCamera;

    public PlayerController()
    {
        Instance = this;
    }

    public void Start()
    {
        orthographicCamera = CameraExtensions.GetMainOrthgraphicCamera();
        orthographicCamera.enabled = false;
        Enabled = false;
    }

    void RotateShipToCursor()
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
        input = GridUtilities.DegreeToVector2(inputRotation);
        ship.rb2d.AddForce(input);
    }

    private void OnEnable()
    {
        InputManager.InputAxis += OnAxis;
        InputManager.UpdateEvent += RotateShipToCursor;
    }

    private void OnDisable()
    {
        InputManager.InputAxis -= OnAxis;
        InputManager.UpdateEvent -= RotateShipToCursor;
    }
}

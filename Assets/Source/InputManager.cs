using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Global event driven inputManager
/// </summary>
public partial class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    public delegate void ButtonActionClickDelegate();
    public static event ButtonActionClickDelegate ClickShift;
    public static event ButtonActionClickDelegate ClickEsc;

    public delegate void ButtonKeypressClickDelegate(char keyPressed);
    public static event ButtonKeypressClickDelegate ClickKeypress;

    public delegate void InputAxisDelegate(Vector2 input);
    public static event InputAxisDelegate InputAxis;

    public InputManager()
    {
        Instance = this;
    }

    void OnGUI()
    {
        var currentEvent = Event.current;
        if (currentEvent.isKey && currentEvent.type == EventType.KeyDown)
        {
            var keyCode = currentEvent.keyCode;
            if (keyCode == KeyCode.None)
            {
                return;
            }
            char key;
            if (TryParseKeycode(keyCode, out key))
            {
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    key = char.ToUpper(key);
                }
                ClickKeypress?.Invoke(key);
            }    
        }
    }

    void Update()
    {
        // ButtonPresses
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            ClickShift?.Invoke();
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ClickEsc?.Invoke();
        }

        // Axis input
        var horizontalAxis = Input.GetAxisRaw("Horizontal");
        var verticalAxis = Input.GetAxisRaw("Vertical");
        if (horizontalAxis != 0f || verticalAxis != 0f)
        {
            InputAxis?.Invoke(new Vector2(horizontalAxis, verticalAxis));
        }
    }
}

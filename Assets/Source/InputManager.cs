using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public delegate void InputDelegate();
    public event InputDelegate Click;

    void Start()
    {
        Click += OnClick;
    }

    void Update()
    {
        if (Input.GetKeyDown("a"))
        {
            Click.Invoke();
        }
    }

    public void OnClick()
    {
        print("a");
    }
}

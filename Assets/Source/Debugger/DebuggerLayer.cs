using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebuggerLayer : MonoBehaviour
{
    public Button loadBlueprintButton;
    public InputField loadBlueprintInputField;

    public void Awake()
    {
        loadBlueprintButton.onClick.AddListener(SpawnShip);
    }

    public void SpawnShip()
    {
        Debugger.SpawnShipByName(loadBlueprintInputField.text);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static partial class Debugger
{
    public static void SetMaxPower(string input)
    {
        float value;
        if (float.TryParse(input, out value)) 
        {
            UIManager.Instance.powerDrawBar.max = value;
        }
        else
        {
            Debug.LogWarning("Invalid format - input float only");
        }
    }

    public static void SetMaxIntegrity(string input)
    {
        float value;
        if (float.TryParse(input, out value))
        {
            UIManager.Instance.integrityDrawBar.max = value;
        }
        else
        {
            Debug.LogWarning("Invalid format - input float only");
        }
    }
}

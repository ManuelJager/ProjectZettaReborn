#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpeedDisplayer : Text
{
    public void UpdateDisplayValue(float value)
    {
        text = $"{Math.Round(value, 1)} M/S";
    }
}

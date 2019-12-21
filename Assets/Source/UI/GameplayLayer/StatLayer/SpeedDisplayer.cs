#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class SpeedDisplayer : MonoBehaviour
{
    [SerializeField] private Text SpeedDisplayText;
    public void UpdateDisplayValue(float value)
    {
        SpeedDisplayText.text = $"{Math.Round(value, 1)} M/S";
    }
}

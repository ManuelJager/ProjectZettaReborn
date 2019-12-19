#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerDrawBar : MonoBehaviour
{
    [SerializeField] private RectTransform barRect;

    public void UpdateBar(float currentHealth, float max)
    {
        barRect.localScale = new Vector3(Mathf.Clamp01(currentHealth / max), 1, 1);
    }
}

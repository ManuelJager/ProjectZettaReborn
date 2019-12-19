#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDrawBar : MonoBehaviour
{
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectTransform drawRect;
    [SerializeField] private RectTransform capacityRect;
    [SerializeField] private Text drawText;
    [SerializeField] private Text capacityText;

    public void UpdateBar(float currentHealth, float max)
    {
        barRect.localScale = new Vector3(Mathf.Clamp01(currentHealth / max), 1, 1);
        drawRect.anchoredPosition = new Vector2(-(capacityRect.sizeDelta.x + 10), 0);
        capacityText.text = $"/{Mathf.Round(max)}";
        drawText.text = Mathf.Round(currentHealth).ToString();
    }
}

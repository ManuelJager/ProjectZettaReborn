#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerDrawBar : MonoBehaviour
{
    [SerializeField] private RectTransform barRect;
    [SerializeField] private RectTransform drawRect;
    [SerializeField] private RectTransform capacityRect;
    [SerializeField] private Text drawText;
    [SerializeField] private Text capacityText;

    public void UpdateBar(float currentDraw, float capacity)
    {
        barRect.localScale = new Vector3(currentDraw / capacity, 1, 1);
        drawRect.anchoredPosition = new Vector2(-(capacityRect.sizeDelta.x + 10), 0);
        capacityText.text = $"/{capacity}KWH";
        drawText.text = currentDraw.ToString();
    }
}

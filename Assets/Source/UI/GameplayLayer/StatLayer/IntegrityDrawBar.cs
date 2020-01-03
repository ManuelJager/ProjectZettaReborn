#pragma warning disable CS0649
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers.ValueDisplayers
{
    public class IntegrityDrawBar : MonoBehaviour, IStatLayerReportable
    {
        [SerializeField] private RectTransform barRect;
        [SerializeField] private Text drawText;

        public float value { get; set; } = 1f;
        public float displayValue { get; set; } = 1f;
        public float max { get; set; } = 1f;
        public float displayMax { get; set; } = 1f;
        public float multiplier { get; set; } = 0.02f;
        public float step { get; set; } = 0.01f;

        public void RefreshDisplayFields()
        {
            displayValue = value;
            displayMax = max;
        }

        public void UpdateBar(float displayValue, float displayMax)
        {
            barRect.localScale = new Vector3(Mathf.Clamp01(displayValue / displayMax), 1, 1);
            drawText.text = $"{Mathf.Round(displayValue / displayMax * 100)}%";
        }

        public void Update()
        {
            displayValue = Zetta.Math.Interpolationf.MixedInterpolate(
                displayValue,
                value,
                multiplier,
                step);

            displayMax = Zetta.Math.Interpolationf.MixedInterpolate(
                displayMax,
                max,
                multiplier,
                step);

            UpdateBar(displayValue, displayMax);
        }
    }
}
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Zetta.UI.Controllers.ValueDisplayers
{
    public class BatteryDisplayer : MonoBehaviour
    {
        public Image displayImage;
        public List<Sprite> sprites;

        public void UpdateDisplayValue(float percentageValue)
        {
            var index = GetIndex(percentageValue);
            displayImage.sprite = sprites[index];
        }

        public int GetIndex(float percentageValue)
        {
            return Mathf.FloorToInt((100f - percentageValue) / 12.5f);
        }
    }
}
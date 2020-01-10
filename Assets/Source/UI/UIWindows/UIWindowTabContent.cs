using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabContent : MonoBehaviour
    {
        [HideInInspector] public RectTransform rectTransform;
        [HideInInspector] public UIWindowTabHeader tabHeader;
        public string tabName;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}


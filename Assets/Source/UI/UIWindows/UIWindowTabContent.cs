using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabContent : MonoBehaviour
    {
        public GameObject tabHeader;
        public RectTransform rectTransform;
        public string tabName;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}


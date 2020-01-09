using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabContent : MonoBehaviour
    {
        [HideInInspector] public GameObject tabHeader;
        [HideInInspector] public RectTransform rectTransform;
        public string tabName;

        private void Start()
        {
            rectTransform = GetComponent<RectTransform>();
        }
    }
}


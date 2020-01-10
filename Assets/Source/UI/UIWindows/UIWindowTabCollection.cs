using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabCollection : List<UIWindowTabContent>
    {
        private UIWindowTabContent activeContent = null;

        public new void Add(UIWindowTabContent tabContent)
        {
            base.Add(tabContent);
            tabContent.tabHeader.Click += () => SetActive(tabContent);
        }

        public new void Remove(UIWindowTabContent tabContent)
        {
            base.Remove(tabContent);
            tabContent.tabHeader.gameObject.SetActive(false);
            tabContent.gameObject.SetActive(false);
        }

        public void SetActive(UIWindowTabContent tabContent)
        {
            if (activeContent != null)
            {
                activeContent.gameObject.SetActive(false);
                activeContent.tabHeader.Selected = false;
            }
            tabContent.gameObject.SetActive(true);
            activeContent = tabContent;
        }
    }
}
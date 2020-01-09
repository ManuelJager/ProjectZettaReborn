#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabManager : MonoBehaviour
    {
        public UIWindowTabCollection tabs;
        public GameObject contentOwner;
        public GameObject headerOwner;
        public GameObject headerPrefab;

        [SerializeField] private GameObject[] tabPrefabs;
        private UIWindowTabContent activeWindow;

        public void Start()
        {
            tabs = new UIWindowTabCollection(this);

            for (int i = 0; i < tabPrefabs.Length; i++)
            {
                Add(tabPrefabs[i]);
            }

            tabs[0].gameObject.SetActive(true);
            activeWindow = tabs[0];
        }

        /// <summary>
        /// Construct the tab content prefab
        /// Creates a header with the name of the content prefab
        /// </summary>
        /// <param name="tab"></param>
        public void Add(GameObject tab)
        {
            var tabContentObject = Instantiate(tab, contentOwner.transform);
            var tabContent = tabContentObject.GetComponent<UIWindowTabContent>();
            var tabHeaderObject = Instantiate(headerPrefab, headerOwner.transform);
            var tabHeader = tabHeaderObject.GetComponent<UIWindowTabHeader>();

            tabContent.tabHeader = tabHeaderObject;
            tabHeader.Text = tabContent.tabName;

            tabHeader.Click += () =>
            {
                activeWindow.gameObject.SetActive(false);
                activeWindow = tabContent;
                tabContent.gameObject.SetActive(true);
            };

            tabs.Add(tabContent);

            tabContentObject.SetActive(false);
        }
    }
}

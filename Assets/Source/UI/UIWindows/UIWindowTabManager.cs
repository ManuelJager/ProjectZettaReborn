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

        public void Awake()
        {
            
        }

        public void Start()
        {
            tabs = new UIWindowTabCollection(this);

            for (int i = 0; i < tabPrefabs.Length; i++)
            {
                Add(tabPrefabs[i]);
            }

            tabs[0].gameObject.SetActive(true);
        }

        public void Add(GameObject tab)
        {
            var tabContentObject = Instantiate(tab, contentOwner.transform);
            var tabContent = tabContentObject.GetComponent<UIWindowTabContent>();
            var tabHeaderObject = Instantiate(headerPrefab, headerOwner.transform);
            var tabHeader = tabHeaderObject.GetComponent<UIWindowTabContent>();

            tabContent.tabHeader = tabHeaderObject;
            tabHeaderObject.GetComponent<UIWindowTabHeader>().Text = tabContent.tabName;

            tabs.Add(tabContent);

            tabContentObject.SetActive(false);
        }
    }
}

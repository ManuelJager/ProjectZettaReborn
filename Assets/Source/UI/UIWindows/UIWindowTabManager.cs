#pragma warning disable CS0649

using UnityEngine;

namespace Zetta.UI.UIWindows
{
    public class UIWindowTabManager : MonoBehaviour
    {
        public UIWindowTabCollection tabs = new UIWindowTabCollection();
        public GameObject contentOwner;
        public GameObject headerOwner;
        public GameObject headerPrefab;

        [SerializeField] private GameObject[] tabPrefabs;
        private UIWindowTabContent activeWindow;

        public void Start()
        {
            for (int i = 0; i < tabPrefabs.Length; i++)
            {
                Add(tabPrefabs[i]);
            }

            tabs.SetActive(tabs[0]);
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

            tabContent.tabHeader = tabHeader;
            tabHeader.tabContent = tabContent;
            tabHeader.Text = tabContent.tabName;

            tabs.Add(tabContent);

            tabContentObject.SetActive(false);
        }
    }
}
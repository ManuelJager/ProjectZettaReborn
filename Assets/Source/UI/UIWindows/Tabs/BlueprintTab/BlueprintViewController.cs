#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem.Blueprints;

namespace Zetta.UI.UIWindows.Tabs.BlueprintTab
{
    public class BlueprintViewController : MonoBehaviour
    {
        [SerializeField] private GameObject blueprintViewPrefab;
        private List<BlueprintView> views = new List<BlueprintView>();

        public BlueprintView Add(BlueprintViewModel viewModel)
        {
            var view = Instantiate(blueprintViewPrefab, transform).GetComponent<BlueprintView>();
            viewModel.view = view;
            view.viewModel = viewModel;
            views.Add(view);
            view.ReflectViewModel();
            return view;
        }

        public void Remove(BlueprintViewModel viewModel)
        {
            views.Remove(viewModel.view);
            Destroy(viewModel.view.gameObject);
        }
    }
}


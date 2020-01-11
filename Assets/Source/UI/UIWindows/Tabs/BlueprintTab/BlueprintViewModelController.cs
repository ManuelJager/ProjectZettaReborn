#pragma warning disable CS0649

using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zetta.GridSystem.Blueprints;

namespace Zetta.UI.UIWindows.Tabs.BlueprintTab
{
    public class BlueprintViewModelController : MonoBehaviour
    {
        private List<BlueprintViewModel> viewModels = new List<BlueprintViewModel>();
        [SerializeField] private BlueprintViewController blueprintViewController;

        public void Add(Blueprint blueprint)
        {
            var viewModel = new BlueprintViewModel(blueprint);
            viewModels.Add(viewModel);
            viewModel.view = blueprintViewController.Add(viewModel);
        }

        public void Remove(Blueprint blueprint)
        {
            var selectedViewModel = viewModels.Where((BlueprintViewModel viewModel) =>
            {
                return viewModel.blueprint == blueprint;
            }).FirstOrDefault();

            viewModels.Remove(selectedViewModel);
            blueprintViewController.Remove(selectedViewModel);
        }

        private void Start()
        {
            Initialize();
        }

        public void Initialize()
        {
            foreach (var blueprint in BlueprintManager.blueprints)
            {
                Add(blueprint);
            }
        }
    }
}
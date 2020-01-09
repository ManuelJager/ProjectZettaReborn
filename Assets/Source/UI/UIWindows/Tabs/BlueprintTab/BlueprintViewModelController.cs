#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.GridSystem;
using Zetta.GridSystem.Blueprints;
using System.Linq;

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

        public void Awake()
        {
            BlueprintManager.loadedBlueprints.BlueprintAdded += Add;
            BlueprintManager.loadedBlueprints.BlueprintRemoved += Remove;
        }

        public void Start()
        {
            foreach (var blueprint in BlueprintManager.loadedBlueprints)
            {
                Add(blueprint);
            }
        }
    }
}

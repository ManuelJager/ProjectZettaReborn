#pragma warning disable CS0649

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.MVVM;


namespace Zetta.GridSystem.Blueprints
{
    [Serializable]
    public class BlueprintViewController : ViewControllerBase<BlueprintModel, BlueprintViewModel, BlueprintView>
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private Transform parent;

        public BlueprintViewController()
            : base(null, null)
        {
            viewFactory = (BlueprintViewModel viewModel) =>
            {
                var go = GameObject.Instantiate(prefab, parent);
                var component = go.GetComponent<BlueprintView>();

                component.Initialize(viewModel);
                component.OnUpdate(viewModel);

                return component;
            };
            destructor = (BlueprintView view) =>
            {
                GameObject.Destroy(view.gameObject);
            };
        }
    }
}

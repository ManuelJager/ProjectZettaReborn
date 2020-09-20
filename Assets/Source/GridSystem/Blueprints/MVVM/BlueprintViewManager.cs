#pragma warning disable CS0649

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintViewManager : MonoBehaviour
    {
        [SerializeField] internal BlueprintViewController userBlueprintViewController;

        private void Start()
        {
            BlueprintManager.Instance.userBlueprintsViewModelController.Connect(userBlueprintViewController);
        }
    }
}
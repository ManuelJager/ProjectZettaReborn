using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintViewModelController : ViewModelControllerBase<BlueprintModel, BlueprintViewModel>
    {
        public BlueprintViewModelController() 
            : base(null)
        {
            viewModelFactory = (BlueprintModel model) =>
            {
                return new BlueprintViewModel(model);
            };
        }
    }
}
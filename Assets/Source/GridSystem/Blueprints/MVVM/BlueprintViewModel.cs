using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintViewModel : ViewModelBase<BlueprintModel>
    {
        internal string name;
        internal int rating;
        internal int blockCount;
        internal Sprite thumbnail;

        public BlueprintViewModel(BlueprintModel model) 
            : base(model)
        {
            OnUpdate = (BlueprintModel updatedModel) =>
            {
                this.name = updatedModel.Name;
                this.rating = 0;
                this.blockCount = updatedModel.Blocks.Count;
            };
            OnUpdate.Invoke(model);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zetta.MVVM;
using Zetta.GridSystem.Blueprints.Thumbnails;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintView : MonoViewBase<BlueprintModel, BlueprintViewModel>
    {
        [SerializeField] protected Text nameTextComponent;
        [SerializeField] protected Text ratingTextComponent;
        [SerializeField] protected Text blockCountTextComponent;
        [SerializeField] protected Image thumbnailImageComponent;

        public BlueprintView()
        {
            // Set update method
            OnUpdate = (BlueprintViewModel updatedViewModel) =>
            {
                nameTextComponent.text = updatedViewModel.name;
                ratingTextComponent.text = updatedViewModel.rating.ToString();
                blockCountTextComponent.text = updatedViewModel.blockCount.ToString();
                if (updatedViewModel.thumbnail == null)
                {
                    updatedViewModel.thumbnail = ThumbnailManager.Instance.GetThumbnail(updatedViewModel.model);
                }
            };
        }
    }
}
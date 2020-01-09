#pragma warning disable CS0649
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zetta.GridSystem.Blueprints;

namespace Zetta.UI.UIWindows.Tabs.BlueprintTab
{
    public class BlueprintView : MonoBehaviour
    {
        public BlueprintViewModel viewModel;
        [SerializeField] private Image thumbnailImage;
        [SerializeField] private Text descriptionName;
        [SerializeField] private Text descriptionRating;
        [SerializeField] private Text descriptionBlockCount;

        public void Update()
        {
            descriptionName.text = viewModel.nameString;
            descriptionRating.text = viewModel.ratingString;
            descriptionBlockCount.text = viewModel.blockCountString;
        }
    }
}
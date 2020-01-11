#pragma warning disable CS4014

using Zetta.GridSystem.Blueprints;

namespace Zetta.UI.UIWindows.Tabs.BlueprintTab
{
    public class BlueprintViewModel
    {
        public readonly string nameString;
        public readonly string ratingString;
        public readonly string blockCountString;
        public readonly Blueprint blueprint;
        public BlueprintView view;

        public BlueprintViewModel(Blueprint blueprint)
        {
            nameString = blueprint.Name;
            ratingString = $"{0} Rating";
            blockCountString = $"{blueprint.Blocks.Count} Blocks";
            this.blueprint = blueprint;
        }

        public bool Visible
        {
            get => view.gameObject.activeSelf;
            set => view.gameObject.SetActive(value);
        }
    }
}
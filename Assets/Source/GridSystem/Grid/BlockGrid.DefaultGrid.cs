using System.Collections.Generic;
using Zetta.GridSystem.Blocks;
using Zetta.GridSystem.Blueprints;

namespace Zetta.GridSystem
{
    public partial class BlockGrid
    {
        [System.Obsolete]
        /// <summary>
        /// Instantiates the default grid
        /// </summary>
        /// <returns>The blocks instantiated</returns>
        public List<GridBlockBase> defaultGrid
        {
            get
            {
                Blueprint defaultBlueprint = BlueprintManager.Import(BlueprintManager.DEFAULT_BLUEPRINT);
                return InstantiateBlueprint(defaultBlueprint);
            }
        }
    }
}
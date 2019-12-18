using Blueprints;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class BlockGrid : MonoBehaviour
    {
        // Unordered list of references to all blocks in grid
        public List<GridBlockBase> uBlockList = null;

        public void Awake()
        {
            GridSizeChanged += UpdateCenterOfMass;
        }

        /// <summary>
        /// Instantiates the blueprint and sets the parent to the current block
        /// </summary>
        /// <param name="blueprint">The blueprint to instantiate</param>
        /// <returns>The objects instantiated</returns>
        public List<GridBlockBase> InstantiateBlueprint(Blueprint blueprint)
        {
            uBlockList = GameManager.Instance.bpInstantiator.InstantiateBlueprint(blueprint, transform);
            return uBlockList;
        }
    }
}

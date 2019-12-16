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
    }
}

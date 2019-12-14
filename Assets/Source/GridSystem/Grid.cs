using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class Grid : MonoBehaviour
    {
        // Unordered list of references to all blocks in grid
        private List<GridBlockBase> uBlockList;
        public Grid(List<GridBlockBase> uBlockList = null)
        {
            this.uBlockList = uBlockList ?? defaultGrid;
        }
    }

}

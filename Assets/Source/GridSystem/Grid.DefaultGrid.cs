using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class Grid
    {
        public static List<GridBlockBase> defaultGrid
        {
            get
            {
                return new List<GridBlockBase> 
                { 
                    new ArmorGridBlock(new Vector2(1, 1), 10, 10, 10) 
                };
            }
        }
    }
}

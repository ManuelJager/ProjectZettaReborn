using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zetta.GridSystem
{

    public class Chunk : List<ZettaEntity>
    {
        public readonly Vector2Int position;
        
        public Chunk(Vector2Int position)
        {
            this.position = position;
        }
    }
}

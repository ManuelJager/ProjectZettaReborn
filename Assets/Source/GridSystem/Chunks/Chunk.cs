using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Zetta.GridSystem
{
    public class Chunk
    {
        public readonly Vector2Int position;

        private List<ZettaEntity> entities;
        
        public Chunk(Vector2Int position)
        {
            this.position = position;
            entities = new List<ZettaEntity>();
        }

        /// <summary>
        /// Adds the entity to this chunk
        /// </summary>
        /// <param name="entity">Entity to add</param>
        public void AddEntity(ZettaEntity entity)
        {
            entities.Add(entity);
        }

        /// <summary>
        /// Removes the entity from this chunk
        /// </summary>
        /// <param name="entity">Entity to remove</param>
        public void RemoveEntity(ZettaEntity entity)
        {
            entities.Remove(entity);
        }
    }
}

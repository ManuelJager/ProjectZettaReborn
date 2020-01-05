using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zetta.Generics;

namespace Zetta.GridSystem
{
    public class ZettaEntityManager : LazySingleton<ZettaEntityManager>
    {
        public List<ZettaEntity> worldEntities;

        public ZettaEntityManager()
        {
            worldEntities = new List<ZettaEntity>();
        }

        public void Update()
        {
            for (int i = 0; i < worldEntities.Count; i++)
            {
                var worldEntity = worldEntities[i];

                worldEntity.CheckVisibilty();
            }
        }

        /// <summary>
        /// Adds the entity to the entity track list
        /// </summary>
        /// <param name="entity"></param>
        public void TrackEntity(ZettaEntity entity)
        {
            worldEntities.Add(entity);
        }

        [RuntimeInitializeOnLoadMethod]
        public static void EchoThis() => Echo();
    }
}

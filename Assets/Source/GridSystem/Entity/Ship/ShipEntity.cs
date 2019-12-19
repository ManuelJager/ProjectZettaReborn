using Blueprints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GridSystem.Entity
{
    public class ShipEntity : GridEntity
    {
        protected Rigidbody2D rigid;

        public void InstantiateShip(Blueprint ship)
        {
            // Instantiate the ship
            InstantiateBlueprint(ship);
            
            // TODO set location of ship etc...
        }

    }
}

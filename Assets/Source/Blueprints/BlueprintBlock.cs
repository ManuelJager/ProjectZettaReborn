using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Blueprints
{
    class BlueprintBlock
    {
        protected string blockTypeID;
        public string BlockTypeID
        {
            get => blockTypeID;
        }

        protected Vector2 position;

        public Dictionary<string, float> Position
        {
            get => new Dictionary<string, float>
            {
                { "x", position.x },
                { "y", position.y }
            };
        }

        protected int rotation;
        public int Rotation
        {
            get => rotation;
        }

        /// <summary>
        /// Blueprint block is a list simplified version of an grid block
        /// </summary>
        /// <param name="blockTypeID">The block type id</param>
        /// <param name="position">The position of the block</param>
        public BlueprintBlock(string blockTypeID, Vector2 position)
        {
            this.blockTypeID = blockTypeID;
            this.position = position;
            this.rotation = 0;
        }

        /// <summary>
        /// Blueprint block is a list simplified version of an grid block
        /// </summary>
        /// <param name="blockTypeID">The block type id</param>
        /// <param name="position">The position of the block</param>
        /// <param name="rotation">The rotation of the block(0,1,2,3)</param>
        public BlueprintBlock(string blockTypeID, Vector2 position, int rotation)
        {
            this.blockTypeID = blockTypeID;
            this.position = position;
            this.rotation = rotation;
        }
    }
}

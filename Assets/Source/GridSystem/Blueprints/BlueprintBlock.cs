﻿using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;
using Zetta.Extensions;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintBlock
    {
        protected string blockTypeID;

        public string BlockTypeID
        {
            get => blockTypeID;
        }

        protected Vector2 position;

        [JsonIgnore]
        public Vector2 VectorPosition
        {
            get => position;
            set => position = value;
        }

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
        [JsonConstructor]
        public BlueprintBlock(string blockTypeID, Vector2 position, int rotation)
        {
            this.blockTypeID = blockTypeID;
            this.position = position;
            this.rotation = rotation;
        }

        public override int GetHashCode()
        {
            var propertyList = new List<object> { blockTypeID, position, rotation };
            return EnumerableExtensions.GetHashCodeAggregate(propertyList);
        }
    }
}
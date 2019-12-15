using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Source.Blueprints
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

        public BlueprintBlock(string blockTypeID, Vector2 position)
        {
            this.blockTypeID = blockTypeID;
            this.position = position;
        }
    }
}

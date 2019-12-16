using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprints
{
    class Blueprint
    {
        protected string name;
        public string Name
        {
            get => name;
        }

        protected List<BlueprintBlock> blocks;
        public List<BlueprintBlock> Blocks
        {
            get => blocks;
        }

        public Blueprint(string name, List<BlueprintBlock> blocks)
        {
            this.name = name;
            this.blocks = blocks;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blueprints
{
    public class Blueprint
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

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var other = (Blueprint)obj;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            var properties = new List<int> 
            { 
                name.GetHashCode(), 
                blocks.GetHashCodeAggregate() 
            };

            return properties.GetHashCodeAggregate();
        }

        public override string ToString()
        {
            return name;
        }

        public bool IsValid
        {
            get => BlueprintManager.ValidateBlueprint(this).Count <= 0;
        }
    }
}

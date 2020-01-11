using Newtonsoft.Json;
using System.Collections.Generic;
using Zetta.Extensions;

namespace Zetta.GridSystem.Blueprints
{
    public class Blueprint
    {
        [JsonIgnore]
        private int? cachedHash;

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

        private int GenerateHashCode()
        {
            var properties = new List<int>
            {
                name.GetHashCode(),
                blocks.GetHashCodeAggregate()
            };

            var hash = properties.GetHashCodeAggregate();
            cachedHash = hash;

            return hash;
        }

        public override int GetHashCode()
        {
            return cachedHash ?? GenerateHashCode();
        }

        public override string ToString()
        {
            return name;
        }

        [JsonIgnore]
        public bool IsValid
        {
            get => BlueprintManager.ValidateBlueprint(this).Count <= 0;
        }
    }
}
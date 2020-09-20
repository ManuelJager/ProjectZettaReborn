using Newtonsoft.Json;
using System.Collections.Generic;
using Zetta.Extensions;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class BlueprintModel : ModelBase
    {
        protected List<BlueprintBlock> blocks;
        protected string name;
        [JsonIgnore] private int? cachedHash;

        public BlueprintModel(string name, List<BlueprintBlock> blocks)
        {
            this.name = name;
            this.blocks = blocks;
        }

        public List<BlueprintBlock> Blocks
        {
            get => blocks;
        }

        [JsonIgnore]
        public bool IsValid
        {
            get => BlueprintManager.ValidateBlueprint(this).Count == 0;
        }

        public string Name
        {
            get => name;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || !GetType().Equals(obj.GetType()))
            {
                return false;
            }
            var other = (BlueprintModel)obj;
            return GetHashCode() == other.GetHashCode();
        }

        public override int GetHashCode()
        {
            return cachedHash ?? GenerateHashCode();
        }

        public override string ToString()
        {
            return name;
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
    }
}
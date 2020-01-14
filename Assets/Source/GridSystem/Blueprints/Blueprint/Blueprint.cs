using Newtonsoft.Json;
using System.Collections.Generic;
using Zetta.Extensions;
using Zetta.MVVM;

namespace Zetta.GridSystem.Blueprints
{
    public class Blueprint
    {
        [JsonIgnore] private int? cachedHash;

        protected string name;
        protected List<BlueprintBlock> blocks;

        public Blueprint(string name, List<BlueprintBlock> blocks)
        {
            this.name = name;
            this.blocks = blocks;
        }

        public string Name
        {
            get => name;
        }

        public List<BlueprintBlock> Blocks
        {
            get => blocks;
        }


        [JsonIgnore]
        public bool IsValid
        {
            get => BlueprintManager.ValidateBlueprint(this).Count <= 0;
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
    }
}
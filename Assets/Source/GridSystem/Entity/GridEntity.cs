using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace GridSystem.Entity
{
    public class GridEntity : BlockGrid
    {
        [SerializeField]
        protected string name;
        public string Name
        {
            get => name;
            set => name = value;
        }
    }
}

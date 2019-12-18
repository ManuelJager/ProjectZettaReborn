using GridSystem;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Events
{
    public class GridSpawnEvent : Event
    {
        private BlockGrid grid;

        public BlockGrid Grid
        {
            get => grid;
            set => grid = value;
        }

        public GridSpawnEvent() { }

        public GridSpawnEvent(BlockGrid grid)
        {
            this.grid = grid;
        }

        public override string Name()
        {
            return "GridSpawnEvent";
        }
    }
}

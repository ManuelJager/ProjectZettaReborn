#pragma warning disable CS0067
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class BlockGrid
    {
        public delegate void BlockGridDelegate(BlockGrid grid);
        public static event BlockGridDelegate BlockGridInstantiatedEvent;

        public delegate void BlockGridSizeChangedDelegate();
        public static event BlockGridSizeChangedDelegate BlockGridSizeChangedEvent;
    }
}
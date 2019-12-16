#pragma warning disable CS0067
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GridSystem
{
    public partial class BlockGrid
    {
        public delegate void GridSizeChangedDelegate();
        public event GridSizeChangedDelegate GridSizeChanged;
    }
}
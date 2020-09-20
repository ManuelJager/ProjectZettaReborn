using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Generics
{
    public delegate void InitializedDelegate();

    public interface IInitializeable
    {
        void Initialize();
    }
}
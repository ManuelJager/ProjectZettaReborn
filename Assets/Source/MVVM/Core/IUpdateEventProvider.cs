using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Zetta.MVVM.Core
{
    public delegate void UpdateDelegate();

    public interface IUpdateEventProvider
    {
        event UpdateDelegate Update;
    }
}
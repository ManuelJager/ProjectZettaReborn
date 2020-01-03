using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Curves
{
    public interface ICurveProvider
    {
        float GetY(float x);
    }
}


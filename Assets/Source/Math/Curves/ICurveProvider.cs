using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Math.Curves
{
    public interface ICurveProvider
    {
        float GetY(float x);
    }
}


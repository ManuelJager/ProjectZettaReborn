using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta
{
    public interface CurveProvider
    {
        float GetY(float x);
    }
}


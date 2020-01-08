using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zetta.Math
{
    public static class Geometryf
    {
        public static float NormalizeValue(float min, float max, float value)
        {
            return ((value - min) / (max - min)) * 2 - 1;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta
{
    public partial struct Math
    {
        public static float MixedInterpolate(float from, float to, float multiplier = 0.5f, float step = 0.5f)
        {
            if (from == to)
                return from;
            var dif = to - from;
            if (dif == 0f)
                return from;
            from += dif * multiplier;
            dif = to - from;
            from += dif > 0 ? dif > step ? step : dif : dif < -step ? -step : dif;
            return from;
        }

        public static Vector2 MixedInterpolatev2(Vector2 from, Vector2 to, float multiplier = 0.5f, float step = 0.5f)
        {
            return new Vector2(
                MixedInterpolate(from.x, to.x, multiplier, step),
                MixedInterpolate(from.y, to.y, multiplier, step));
        }

        public static Vector3 MixedInterpolatev3(Vector3 from, Vector3 to, float multiplier = 0.5f, float step = 0.5f)
        {
            return new Vector3(
                MixedInterpolate(from.x, to.x, multiplier, step),
                MixedInterpolate(from.y, to.y, multiplier, step),
                MixedInterpolate(from.z, to.z, multiplier, step));
        }
    }
}

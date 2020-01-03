using UnityEngine;

namespace Zetta.Math
{
    public class Vectorf
    {
        /// <summary>
        /// Rotates a vector2 along the z axis
        /// </summary>
        public static Vector2 RotateVector2(Vector2 vector, float angle)
        {
            var theta = angle * Mathf.Deg2Rad;

            var cs = Mathf.Cos(theta);
            var sn = Mathf.Sin(theta);

            var px = vector.x * cs - vector.y * sn;
            var py = vector.x * sn + vector.y * cs;

            return new Vector2(px, py);
        }

        public static Vector2 RadianToVector2(float radian)
        {
            return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
        }

        public static Vector2 DegreeToVector2(float degree)
        {
            return RadianToVector2(degree * Mathf.Deg2Rad);
        }
    }
}
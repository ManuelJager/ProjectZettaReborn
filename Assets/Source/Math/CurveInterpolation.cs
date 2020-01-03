using UnityEngine;
using System.Collections;

namespace Zetta
{
    public static class CurveInterpolation
    {
        public static void DeltaCurveInterpolate(
                this CurveProvider curve,
                float duration,
                System.Action<float> callback)
        {
            curve.DeltaCurveInterpolate(
                0f,
                1f,
                duration,
                callback);
        }

        public static void DeltaCurveInterpolate(
            this CurveProvider curve,
            float from,
            float to,
            float duration,
            System.Action<float> callback)
        {
            var t = 0f;
            var start = from;
            var length = to - from;

            MonoInstance.Instance.StartCoroutine(
                DeltaCurveInterpolate(
                    curve,
                    start,
                    length,
                    duration,
                    t,
                    callback));
        }

        private static IEnumerator DeltaCurveInterpolate(
            CurveProvider curve,
            float start,
            float length,
            float duration,
            float t,
            System.Action<float> callback)
        {
            if (t < duration)
            {
                callback(start + curve.GetY(t / duration) * length);
                yield return new WaitForEndOfFrame();
                t += Time.deltaTime;

                MonoInstance.Instance.StartCoroutine(
                    DeltaCurveInterpolate(
                        curve,
                        start,
                        length,
                        duration,
                        t,
                        callback));
            }
        }
    }

}

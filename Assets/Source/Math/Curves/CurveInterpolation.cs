using UnityEngine;
using System.Collections;
using UniRx.Async;
using System.Threading;
using System.Threading.Tasks;

namespace Zetta.Curves
{
    public static class CurveInterpolation
    {
        public static async UniTask DeltaCurveInterpolate(
            this ICurveProvider curve,
            float duration,
            System.Action<float> callback)
        {
            await DeltaCurveInterpolate(
                curve,
                0f,
                1f,
                duration,
                callback);
        }

        public static async UniTask DeltaCurveInterpolate(
            this ICurveProvider curve,
            float start,
            float length,
            float duration,
            System.Action<float> callback)
        {
            var t = 0f;
            while (t < duration)
            {
                callback(start + curve.GetY(t / duration) * length);
                await UniTask.DelayFrame(1);
                t += Time.deltaTime;
            }
        }
    }

}

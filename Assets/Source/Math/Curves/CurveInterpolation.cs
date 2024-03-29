﻿using UniRx.Async;
using UnityEngine;

namespace Zetta.Math.Curves
{
    public static class CurveInterpolationf
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
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.Curves;
using System.Threading;
using System.Threading.Tasks;
using UniRx.Async;

namespace Zetta.UI
{
    public static class FadingUtilities
    {
        public static async UniTask FadeIn(
            CanvasGroup group,
            ICurveProvider curve,
            float fadeTime,
            System.Action onEnd)
        {
            await curve.DeltaCurveInterpolate(
                fadeTime,
                (float value) =>
                {
                    group.alpha = value;
                });
            onEnd?.Invoke();
        }

        public static async UniTask FadeOut(
            CanvasGroup group, 
            ICurveProvider curve, 
            float fadeTime,
            System.Action onEnd)
        {
            await curve.DeltaCurveInterpolate(
                fadeTime,
                (float value) =>
                {
                    group.alpha = 1f - value;
                });
            onEnd?.Invoke();
        }
    }
}
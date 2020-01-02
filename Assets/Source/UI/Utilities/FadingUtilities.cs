using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.UI
{
    public class FadingUtilities : AutoInstanceMonoBehaviour<FadingUtilities>
    {
        public static void FadeIn(CanvasGroup group, Math.BezierCurve curve, float duration)
        {
            var t = 0f;
            Instance.StartCoroutine(Instance.FadeIn(group, curve, t, duration));
        }

        public static void FadeOut(CanvasGroup group, Math.BezierCurve curve, float duration)
        {
            var t = 0f;
            Instance.StartCoroutine(Instance.FadeOut(group, curve, t, duration));
        }

        public IEnumerator FadeIn(CanvasGroup group, Math.BezierCurve curve, float t, float duration)
        {
            t += Time.deltaTime;
            if (t > duration)
            {
                yield break;
            }

            group.alpha = curve.GetY(t / duration);
            yield return new WaitForEndOfFrame();
            Instance.StartCoroutine(FadeIn(group, curve, t, duration));
        }

        public IEnumerator FadeOut(CanvasGroup group, Math.BezierCurve curve, float t, float duration)
        {
            t += Time.deltaTime;
            if (t > duration)
            {
                yield break;
            }

            group.alpha = 1f - curve.GetY(t / duration);
            yield return new WaitForEndOfFrame();
            Instance.StartCoroutine(FadeOut(group, curve, t, duration));
        }
    }
}
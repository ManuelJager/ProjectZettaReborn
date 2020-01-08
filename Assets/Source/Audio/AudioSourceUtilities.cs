using System;
using System.Threading;
using UniRx.Async;
using UnityEngine;

namespace Zetta.Audio.Atmosphere
{
    public static class AudioSourceUtilities
    {
        public static async UniTask RaiseOnClipEnd(AudioSource audioSource, Action callback, CancellationToken token)
        {
            await UniTask.DelayFrame(1);
            while (audioSource.isPlaying)
            {
                await UniTask.DelayFrame(1);
            }
            if (!token.IsCancellationRequested)
            {
                callback();
            }
        }
    }
}
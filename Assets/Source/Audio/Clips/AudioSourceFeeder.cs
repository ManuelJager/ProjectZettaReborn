#pragma warning disable CS4014

using System.Threading;
using UnityEngine;

namespace Zetta.Audio.Clips
{
    internal class AudioSourceFeeder
    {
        private AudioSource audioSource;
        private IClipProvider clips;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        private bool promptSoundEffect;

        public AudioSourceFeeder(AudioSource audioSource, IClipProvider clips)
        {
            this.audioSource = audioSource;
            this.clips = clips;
        }

        public AudioSourceFeeder(AudioSource audioSource, IClipProvider clips, bool promptSoundEffect)
        {
            this.audioSource = audioSource;
            this.clips = clips;
            this.promptSoundEffect = promptSoundEffect;
        }

        public void Start()
        {
            if (!audioSource.isPlaying)
            {
                QueueNext(tokenSource.Token);
            }
        }

        public void Stop()
        {
            tokenSource.Cancel();
            audioSource.Stop();
        }

        private void QueueNext(CancellationToken token)
        {
            var clip = clips.GetClip();
            audioSource.clip = clip;
            audioSource.Play();

            if (promptSoundEffect)
            {
                UI.NoticeManager.Instance.Prompt($"Now playing {clip.name}", 2.5f);
            }

            AudioSourceUtilities.RaiseOnClipEnd(audioSource, () => QueueNext(token), token);
        }
    }
}
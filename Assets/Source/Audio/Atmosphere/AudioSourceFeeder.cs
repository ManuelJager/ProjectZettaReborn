#pragma warning disable CS4014

using System.Threading;
using UnityEngine;
using Zetta.Audio.Clips;
using Zetta.Audio.Controllers;

namespace Zetta.Audio.Atmosphere
{
    public class AudioSourceFeeder : IAudioSourcePlayer
    {
        private AudioSource audioSource;
        private IClipProvider clipProvider;
        private CancellationTokenSource tokenSource = new CancellationTokenSource();
        protected bool prompt;
        protected bool isRunning;

        public AudioSourceFeeder(AudioSource audioSource, IClipProvider clipProvider)
        {
            this.audioSource = audioSource;
            this.clipProvider = clipProvider;
            this.prompt = false;
        }

        public AudioSourceFeeder(AudioSource audioSource, IClipProvider clipProvider, bool prompt)
        {
            this.audioSource = audioSource;
            this.clipProvider = clipProvider;
            this.prompt = prompt;
        }

        ~AudioSourceFeeder()
        {
            Stop();
        }

        public bool Prompt
        {
            get => prompt;
        }

        public bool IsRunning
        {
            get => isRunning;
        }

        public void Start()
        {
            if (!audioSource.isPlaying)
            {
                QueueNext(tokenSource.Token);
                isRunning = true;
            }
        }

        public void Stop()
        {
            tokenSource.Cancel();
            audioSource.Stop();
            isRunning = false;
        }

        /// <summary>
        /// Play song and queue the next one when the current finishes
        /// </summary>
        /// <param name="token"></param>
        private void QueueNext(CancellationToken token)
        {
            var clip = clipProvider.GetClip();
            audioSource.clip = clip;
            audioSource.Play();

            if (Prompt)
            {
                UI.NoticeManager.Instance.Prompt($"Now playing {clip.name}", 2.5f);
            }

            // call itself on clip end
            AudioSourceUtilities.RaiseOnClipEnd(audioSource, () => QueueNext(token), token);
        }
    }
}
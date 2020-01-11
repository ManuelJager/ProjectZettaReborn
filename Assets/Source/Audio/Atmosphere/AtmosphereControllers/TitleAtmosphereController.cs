using UnityEngine;
using Zetta.Audio.Controllers;

namespace Zetta.Audio.Atmosphere.AtmosphereControllers
{
    public class TitleAtmosphereController : IAudioSourceController
    {
        private AudioSource audioSource;
        private AudioClip audioClip;
        private bool prompt;

        public TitleAtmosphereController(AudioSource audioSource, AudioClip audioClip)
        {
            this.audioSource = audioSource;
            this.audioClip = audioClip;
            prompt = false;
        }

        public TitleAtmosphereController(AudioSource audioSource, AudioClip audioClip, bool prompt)
        {
            this.audioSource = audioSource;
            this.audioClip = audioClip;
            this.prompt = prompt;
        }

        public bool Prompt => throw new System.NotImplementedException();

        public bool IsRunning
        {
            get => audioSource.isPlaying;
        }

        public void Start()
        {
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }
    }
}
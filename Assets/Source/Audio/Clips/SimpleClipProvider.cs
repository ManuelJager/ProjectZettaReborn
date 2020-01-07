using UnityEngine;

namespace Zetta.Audio.Clips
{
    internal class SingleClipProvider : IClipProvider
    {
        private AudioClip audioClip;

        public SingleClipProvider(AudioClip audioClip)
        {
            this.audioClip = audioClip;
        }

        public AudioClip GetClip()
        {
            return audioClip;
        }
    }
}
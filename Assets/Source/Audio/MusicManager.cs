#pragma warning disable CS0649
#pragma warning disable CS4014

using UniRx.Async;
using UnityEngine;
using Zetta.Audio.Clips;
using Zetta.Generics;
using Zetta.Math.Curves;

namespace Zetta.Audio
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : AutoInstanceMonoBehaviour<MusicManager>
    {
        public static float maxVolume = 1f;

        [SerializeField] private AudioClip titleSong;
        [SerializeField] private RandomClipProvider gameplaySongs;
        private AudioSource audioSource;
        private AudioSourceFeeder audioSourceFeeder;
        private Atmosphere currentAtmosphere;
        private bool fading = false;

        public enum Atmosphere
        {
            Title,
            Gameplay
        }

        public void SetAtmosphere(Atmosphere value, bool fade = false)
        {
            if (currentAtmosphere == value)
            {
                return;
            }

            if (fade)
            {
                Fade(value);
            }
            else
            {
                SwitchAtmosphere(value);
            }
        }

        private async UniTask Fade(Atmosphere newAtmosphere)
        {
            while (fading)
            {
                await UniTask.DelayFrame(1);
            }
            fading = true;

            var curve = BezierCurve.EaseInOut;
            await curve.Reverse.DeltaCurveInterpolate(0f, maxVolume, 2f, (float value) =>
            {
                audioSource.volume = value;
            });

            SwitchAtmosphere(newAtmosphere);

            await curve.DeltaCurveInterpolate(0f, maxVolume, 2f, (float value) =>
            {
                audioSource.volume = value;
            });

            fading = false;
        }

        private void SwitchAtmosphere(Atmosphere value)
        {
            switch (currentAtmosphere)
            {
                case Atmosphere.Title:
                    audioSource.Stop();
                    break;

                case Atmosphere.Gameplay:
                    audioSourceFeeder.Stop();
                    break;

                default:
                    break;
            }

            currentAtmosphere = value;

            switch (currentAtmosphere)
            {
                case Atmosphere.Title:
                    audioSource.clip = titleSong;
                    audioSource.Play();
                    break;

                case Atmosphere.Gameplay:
                    audioSourceFeeder.Start();
                    break;

                default:
                    break;
            }
        }

        private new void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();
            audioSourceFeeder = new AudioSourceFeeder(audioSource, gameplaySongs, true);
        }

        private void Start()
        {
            SetAtmosphere(Atmosphere.Gameplay);
        }
    }
}
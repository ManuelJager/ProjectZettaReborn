#pragma warning disable CS0649
#pragma warning disable CS4014

using UniRx.Async;
using UnityEngine;
using Zetta.Audio.Clips;
using Zetta.Generics;
using Zetta.Math.Curves;
using Zetta.Audio.Atmosphere.AtmosphereControllers;
using System;
using System.Collections;
using System.Collections.Generic;
using Zetta.Audio.Controllers;

namespace Zetta.Audio.Atmosphere
{
    [RequireComponent(typeof(AudioSource))]
    public class MusicManager : AutoInstanceMonoBehaviour<MusicManager>
    {
        public static float maxVolume = 1f;

        [SerializeField] private AudioClip titleSong;
        [SerializeField] private RandomClipProvider gameplaySongs;
        private AudioSource audioSource;
        private Atmosphere currentAtmosphere = Atmosphere.Title;
        private bool fading = false;
        private Dictionary<Atmosphere, IAudioSourceController> atmosphereControllers =
            new Dictionary<Atmosphere, IAudioSourceController>();

        public enum Atmosphere
        {
            Title,
            Gameplay
        }

        private async UniTask SetAtmosphere(Atmosphere newAtmosphere, bool immidiate = true)
        {
            if (currentAtmosphere == newAtmosphere) return;

            var curve = BezierCurve.EaseInOut;

            if (!immidiate)
            {
                while (fading)
                {
                    await UniTask.DelayFrame(1);
                }
                fading = true;
                
                await curve.Reverse.DeltaCurveInterpolate(0f, maxVolume, 2f, (float value) =>
                {
                    audioSource.volume = value;
                });
            }

            atmosphereControllers[currentAtmosphere].Stop();
            currentAtmosphere = newAtmosphere;
            atmosphereControllers[currentAtmosphere].Start();

            if (!immidiate)
            {
                await curve.DeltaCurveInterpolate(0f, maxVolume, 2f, (float value) =>
                {
                    audioSource.volume = value;
                });

                fading = false;
            }
        }

        private new void Awake()
        {
            base.Awake();
            audioSource = GetComponent<AudioSource>();

            atmosphereControllers[Atmosphere.Gameplay] = 
                new GameplayAtmosphereController(audioSource, gameplaySongs, true);

            atmosphereControllers[Atmosphere.Title] = 
                new TitleAtmosphereController(audioSource, titleSong);
        }

        private void Start()
        {
            SetAtmosphere(Atmosphere.Gameplay);
        }
    }
}
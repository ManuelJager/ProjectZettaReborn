using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zetta.Audio.Clips;
using Zetta.Audio.Controllers;

namespace Zetta.Audio.Atmosphere.AtmosphereControllers
{
    public class GameplayAtmosphereController : AudioSourceFeeder, IAudioSourceController
    {
        public GameplayAtmosphereController(AudioSource audioSource, IClipProvider clipProvider)
            : base(audioSource, clipProvider)
        {
        }

        public GameplayAtmosphereController(AudioSource audioSource, IClipProvider clipProvider, bool prompt)
            : base(audioSource, clipProvider, prompt)
        {
        }
    }
}


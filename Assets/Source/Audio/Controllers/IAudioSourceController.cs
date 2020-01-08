using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Audio.Controllers
{
    public interface IAudioSourceController : IAudioSourcePlayer
    {
        void Start();
        void Stop();
    }
}

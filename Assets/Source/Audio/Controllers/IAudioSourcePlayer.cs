using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Audio.Controllers
{
    public interface IAudioSourcePlayer
    {
        bool Prompt { get; }
        bool IsRunning { get; }
    }
}

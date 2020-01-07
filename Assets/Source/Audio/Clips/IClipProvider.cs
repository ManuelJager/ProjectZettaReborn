using UnityEngine;

namespace Zetta.Audio.Clips
{
    /// <summary>
    /// Provides a clip
    /// </summary>
    internal interface IClipProvider
    {
        AudioClip GetClip();
    }
}
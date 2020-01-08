using UnityEngine;

namespace Zetta.Audio.Clips
{
    /// <summary>
    /// Provides a clip
    /// </summary>
    public interface IClipProvider
    {
        AudioClip GetClip();
    }
}
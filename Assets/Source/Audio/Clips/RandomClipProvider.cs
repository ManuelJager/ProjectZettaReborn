#pragma warning disable CS0649

using System.Collections.Generic;
using UnityEngine;

namespace Zetta.Audio.Clips
{
    [System.Serializable]
    public class RandomClipProvider : IObservableClipProvider
    {
        private static System.Random rng = new System.Random();

        [SerializeField] private List<AudioClip> clips;
        private int index = 0;
        private bool shuffled = false;

        /// <summary>
        /// Index of currently playing song
        /// </summary>
        private int Index
        {
            get => index;
            set => index = value % clips.Count;
        }

        /// <summary>
        /// Shuffle clip list using the Fisher-Yates shuffle algorithm
        /// </summary>
        public void Shuffle()
        {
            int iterIndex = clips.Count;
            // Loops over list
            while (iterIndex > 1)
            {
                iterIndex--;
                // Selects index of value to be swapped with current
                int newValueIndex = rng.Next(iterIndex + 1);

                // Swap values
                var value = clips[newValueIndex];
                clips[newValueIndex] = clips[iterIndex];
                clips[iterIndex] = value;
            }
        }

        /// <summary>
        /// Returns randomized clip
        /// </summary>
        public AudioClip GetClip()
        {
            if (!shuffled)
            {
                Shuffle();
                shuffled = true;
            }

            return clips[Index++];
        }
    }
}
using System;
using UnityEngine;

namespace Zetta.Exceptions
{
    internal class ChunkNotAvailableException : Exception
    {
        public ChunkNotAvailableException()
            : base("A chunk was not found.")
        {
        }

        public ChunkNotAvailableException(Vector2 position)
            : base($"Chunk {position.x}, {position.y} was not found.")
        {
        }
    }
}
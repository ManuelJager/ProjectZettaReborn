using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;
using Unity.Entities;

namespace GridSystem
{
    public class GridBlockBase : MonoBehaviour
    {
        [SerializeField]
        public BlockSideRenderer blockSideRenderer;

        [SerializeField]
        protected string blockTypeID;
        public string BlockTypeID
        {
            get => blockTypeID;
        }

        [SerializeField]
        private Vector2 size;
        public Vector2 Size
        {
            get => size;
            set => size = value;
        }

        [SerializeField]
        private Vector2[] blockPositions;
        public Vector2[] BlockPositions
        {
            get => blockPositions;
            set => blockPositions = value;
        }

        [SerializeField]
        private float mass;
        public float Mass
        {
            get => mass;
            set => mass = value;
        }
    }
}

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
        public GridBlockBase(Vector2 size, float mass)
        {
            this.size = size;
            this.mass = mass;
        }

        [SerializeField]
        private Vector2 size;
        public Vector2 Size
        {
            get => size;
            set => size = value;
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

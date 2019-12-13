using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class GridBlockBase : MonoBehaviour
{   
    public GridBlockBase(Vector2 size, float mass)
    {
        this.size = size;
        this.mass = mass;
    }

    [SerializeField]
    public Vector2 size;
    public Vector2 Size
    {
        get => size;
        set => size = value;
    }

    [SerializeField]
    public float mass;
    public float Mass
    {
        get => mass;
        set => mass = value;
    }
}

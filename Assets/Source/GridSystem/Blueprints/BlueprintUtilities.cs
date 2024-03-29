﻿using UnityEngine;

namespace Zetta.GridSystem.Blueprints
{
    public struct MinMaxVector2
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public MinMaxVector2(float minX, float maxX, float minY, float maxY)
        {
            this.minX = minX;
            this.maxX = maxX;
            this.minY = minY;
            this.maxY = maxY;
        }

        public void Apply(Vector2[] vectors)
        {
            for (int i = 0; i < vectors.Length; i++)
            {
                if (vectors[i].x > maxX)
                {
                    maxX = vectors[i].x;
                }
                if (vectors[i].y > maxY)
                {
                    maxY = vectors[i].y;
                }
                if (vectors[i].x < minX)
                {
                    minX = vectors[i].x;
                }
                if (vectors[i].y < minY)
                {
                    minY = vectors[i].y;
                }
            }
        }
    }

    public static class BlueprintUtilities
    {
        public static Bounds GetBounds(BlueprintModel blueprint, bool useUnityAPI = false)
        {
            if (useUnityAPI)
            {
                return GetBoundsUnityAPI(blueprint);
            }
            else
            {
                return GetBoundsNormal(blueprint);
            }
        }

        public static Bounds GetBoundsUnityAPI(this BlueprintModel blueprint)
        {
            var count = blueprint.Blocks.Count;
            if (count > 0)
            {
                var bounds = GetBounds(blueprint.Blocks[0]);
                if (count > 1)
                {
                    for (int i = 1; i < count; i++)
                    {
                        bounds.Encapsulate(GetBounds(blueprint.Blocks[i]));
                    }
                }
                return bounds;
            }
            else
            {
                throw new System.Exception("Cannot get bounds of empty blueprint");
            }
        }

        private static Bounds GetBounds(BlueprintBlock blueprintBlock)
        {
            return new Bounds(
                GetBlueprintPosition(blueprintBlock),
                GetBlueprintBlockSize(blueprintBlock));
        }

        private static Vector3 GetBlueprintPosition(BlueprintBlock blueprintBlock)
        {
            return new Vector3(
                blueprintBlock.position.x,
                blueprintBlock.position.y);
        }

        private static Vector3 GetBlueprintBlockSize(BlueprintBlock blueprintBlock)
        {
            var size = blueprintBlock.runtimeValues.size;

            if (blueprintBlock.Rotation % 2 == 1)
            {
                size = new Vector2(size.y, size.x);
            }

            return new Vector3(size.x, size.y);
        }

        private static Bounds GetBoundsNormal(this BlueprintModel blueprint)
        {
            var corners = GetBlueprintCorners(blueprint);
            var size = GetSize(corners);
            var sizev3 = new Vector3(size.x, size.y);
            var center = GetCenter(corners);
            var centerv3 = new Vector3(center.x, center.y);
            var bounds = new Bounds(centerv3, sizev3);
            return bounds;
        }

        public static Vector2 GetSize(this BlueprintModel blueprint)
        {
            var blueprintCorners = new MinMaxVector2(0f, 0f, 0f, 0f);
            var corners = GetCorners(blueprint);
            blueprintCorners.Apply(corners);

            return new Vector2(
                blueprintCorners.maxX - blueprintCorners.minX,
                blueprintCorners.maxY - blueprintCorners.minY);
        }

        public static Vector2 GetSize(Vector2[] cornerArray)
        {
            var blueprintCorners = new MinMaxVector2(0f, 0f, 0f, 0f);
            blueprintCorners.Apply(cornerArray);

            return new Vector2(
                blueprintCorners.maxX - blueprintCorners.minX,
                blueprintCorners.maxY - blueprintCorners.minY);
        }

        public static Vector2 GetSize(MinMaxVector2 blueprintCorners)
        {
            return new Vector2(
                blueprintCorners.maxX - blueprintCorners.minX,
                blueprintCorners.maxY - blueprintCorners.minY);
        }

        public static MinMaxVector2 GetBlueprintCorners(BlueprintModel blueprint)
        {
            var blueprintCorners = new MinMaxVector2(0f, 0f, 0f, 0f);
            var corners = GetCorners(blueprint);
            blueprintCorners.Apply(corners);

            return blueprintCorners;
        }

        public static MinMaxVector2 GetBlueprintCorners(Vector2[] cornerArray)
        {
            var blueprintCorners = new MinMaxVector2(0f, 0f, 0f, 0f);
            blueprintCorners.Apply(cornerArray);

            return blueprintCorners;
        }

        public static Vector2[] GetCorners(BlueprintModel blueprint)
        {
            var count = blueprint.Blocks.Count;
            var corners = new Vector2[blueprint.Blocks.Count * 4];

            for (int i = 0; i < count; i++)
            {
                var block = blueprint.Blocks[i];
                var size = block.runtimeValues.size;

                if (block.Rotation % 2 == 1)
                {
                    size = new Vector2(size.y, size.x);
                }

                var halfWidth = size.x / 2f;
                var halfHeight = size.y / 2f;

                var arrayBaseIndex = i * 4;

                corners[0 + arrayBaseIndex] = new Vector2(
                    block.VectorPosition.x - halfWidth,
                    block.VectorPosition.y - halfHeight);

                corners[1 + arrayBaseIndex] = new Vector2(
                    block.VectorPosition.x + halfWidth,
                    block.VectorPosition.y - halfHeight);

                corners[2 + arrayBaseIndex] = new Vector2(
                    block.VectorPosition.x - halfWidth,
                    block.VectorPosition.y + halfHeight);

                corners[3 + arrayBaseIndex] = new Vector2(
                    block.VectorPosition.x + halfWidth,
                    block.VectorPosition.y + halfHeight);
            }

            return corners;
        }

        public static Vector2 GetCenter(MinMaxVector2 blueprintCorners)
        {
            var size = GetSize(blueprintCorners);
            return new Vector2(
                blueprintCorners.minX + size.x / 2f,
                blueprintCorners.minY + size.y / 2f);
        }
    }
}
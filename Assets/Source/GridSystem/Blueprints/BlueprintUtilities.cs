using UnityEngine;

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
        public static Vector2 GetSize(this Blueprint blueprint)
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

        public static MinMaxVector2 GetBlueprintCorners(Blueprint blueprint)
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

        public static Vector2[] GetCorners(Blueprint blueprint)
        {
            var count = blueprint.Blocks.Count;
            var corners = new Vector2[blueprint.Blocks.Count * 4];

            for (int i = 0; i < count; i++)
            {
                var block = blueprint.Blocks[i];
                var size = block.size;

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
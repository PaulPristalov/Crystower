using UnityEngine;

namespace GameScene.LevelGeneration
{
    public class PerlinNoise
    {
        public Vector2Int Size { get; private set; } = new(32, 32);
        public Vector2 Offset { get; private set; } = Vector2Int.zero;
        public float Scale { get; private set; } = 5f;
        public float[,] Values { get; private set; }

        public PerlinNoise(Vector2Int size, Vector2 offset, float scale)
        {
            Size = size;
            Offset = offset;
            Scale = scale;
            Generate();
        }

        public void Generate()
        {
            Values = new float[Size.x, Size.y];

            for (int x = 0; x < Size.x; x++)
            {
                for (int y = 0; y < Size.y; y++)
                {
                    Values[x, y] = PerlinPixel(x, y);
                }
            }
        }

        public float PerlinPixel(int x, int y)
        {
            float noiseX = (float)x / Size.x * Scale + Offset.x;
            float noiseY = (float)y / Size.y * Scale + Offset.y;

            return Mathf.PerlinNoise(noiseX, noiseY);
        }
    }
}

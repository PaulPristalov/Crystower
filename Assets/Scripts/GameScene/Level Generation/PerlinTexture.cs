using UnityEngine;
namespace GameScene.LevelGeneration
{
    public class PerlinTexture : MonoBehaviour
    {
        private void Start()
        {
            Renderer renderer = GetComponent<Renderer>();
            PerlinNoise noise = new(new Vector2Int(16, 16), Vector2.zero, 10);
            Texture2D texture = new(16, 16);
            for (int x = 0; x < 16; x++)
            {
                for (int y = 0; y < 16; y++)
                {
                    float value = noise.Values[x, y];
                    Color color = new(value, value, value);
                    texture.SetPixel(x, y, color);
                }
            }
            texture.Apply();
            renderer.material.mainTexture = texture;
        }
    }
}

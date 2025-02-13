using UnityEngine;
using UnityEngine.Events;

namespace GameScene.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Generation Params")]
        [SerializeField] private Vector2Int _size = new(16, 16);

        [Header("Perlin Noise")]
        [SerializeField] private Vector2Int _noiseOffset = Vector2Int.zero;
        [SerializeField] private float _noiseScale = 10f;
        [Range(0f, 1f)][SerializeField] private float _waterValue = .2f;
        [Range(0f, 1f)][SerializeField] private float _mountainValue = .7f;

        [Header("Tilemaps")]
        [SerializeField] private Transform _groundParent;

        [Header("Factories")]
        [SerializeField] private TilesFactory _tilesFactory;

        private Vector2 _offset => new(-_size.x / 2f, -_size.y / 2f);

        public event UnityAction OnLevelFinished;

        private void OnValidate()
        {
            _size.x = Mathf.Clamp(_size.x, 1, 1000);
            _size.y = Mathf.Clamp(_size.y, 1, 1000);
        }

        public void Generate()
        {
            PlaceTiles();
            //PlaceDecoration();
            //PlaceCrystal();
            //PlaceCreatures();

            OnLevelFinished?.Invoke();
        }

        private void PlaceTiles()
        {
            for (int i = 0; i < _groundParent.childCount; i++)
            {
                Destroy(_groundParent.GetChild(i).gameObject);
            }

            _noiseOffset = new(Random.Range(0, 1000000), Random.Range(0, 1000000));
            PerlinNoise noise = new(_size, _noiseOffset, _noiseScale);

            for (int y = 0; y < _size.y; y++)
            {
                for (int x = 0; x < _size.x; x++)
                {
                    GameObject tile = _tilesFactory.GetTile(TileType.Ground);
                    Vector3 position = new(x + _offset.x, transform.position.y, y + _offset.y);
                    position += tile.transform.localScale / 2;
                    float pixel = noise.PerlinPixel(x, y);

                    if (pixel <= _waterValue)
                    {
                        tile = _tilesFactory.GetTile(TileType.Water);
                    }
                    else if (pixel >= _mountainValue)
                    {
                        Instantiate(tile, position, Quaternion.identity, _groundParent);
                        tile = _tilesFactory.GetTile(TileType.Mountain);
                        position.y += .5f;
                    }

                    Instantiate(tile, position, Quaternion.identity, _groundParent);
                }
            }
        }

        private void PlaceDecoration()
        {
            throw new System.NotImplementedException();
        }
        private void PlaceCrystal()
        {
            throw new System.NotImplementedException();
        }

        private void PlaceCreatures()
        {
            throw new System.NotImplementedException();
        }
    }
}

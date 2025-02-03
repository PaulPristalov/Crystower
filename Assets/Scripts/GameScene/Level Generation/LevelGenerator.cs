using UnityEngine;
using UnityEngine.Events;

namespace GameScene.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Generation Params")]
        [SerializeField] private int _seed = 0;
        [SerializeField] private Vector2Int _size = new(16, 16);

        [Header("Perlin Noise")]
        [SerializeField] private Vector2Int _noiseOffset = Vector2Int.zero;
        [SerializeField] private float _noiseScale = 10f;
        [Range(0f, 1f)][SerializeField] private float _waterValue = .2f;
        [Range(0f, 1f)][SerializeField] private float _mountainValue = .7f;

        [Header("Tilemaps")]
        [SerializeField] private Transform _groundParent;

        [Header("Factories")]
        [SerializeField] private TilesFacroty _tilesFacroty;

        [SerializeField] private Renderer _renderer;

        private Vector2Int _offset => -_size / 2;

        public event UnityAction OnLevelFinished;

        private void OnValidate()
        {
            if (_size.x <= 0)
            {
                Debug.LogError("Level size can't be less or equal 0!");
                _size.x = 1;
            }
            if (_size.y <= 0)
            {
                Debug.LogError("Level size can't be less or equal 0!");
                _size.y = 1;
            }
        }

        private void Start()
        {
            if (_seed == 0)
            {
                _seed = Random.Range(int.MinValue, int.MaxValue);
            }
            Random.InitState(_seed);

            Generate();
        }

        public void Generate()
        {
            PlaceTiles();
            //PlaceDecoration();
            //PlaceCrystal();
            //PlaceResources();
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
                    Vector3 position = new(x + _offset.x, transform.position.y, y + _offset.y);
                    GameObject tile = _tilesFacroty.GetTile(TileType.Ground);
                    float pixel = noise.PerlinPixel(x, y);

                    if (pixel <= _waterValue)
                    {
                        tile = _tilesFacroty.GetTile(TileType.Water);
                    }
                    else if (pixel >= _mountainValue)
                    {
                        Instantiate(tile, position, Quaternion.identity, _groundParent);
                        tile = _tilesFacroty.GetTile(TileType.Mountain);
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
        private void PlaceResources()
        {
            throw new System.NotImplementedException();
        }

        private void PlaceCreatures()
        {
            throw new System.NotImplementedException();
        }
    }
}

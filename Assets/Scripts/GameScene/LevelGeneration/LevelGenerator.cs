using System.Linq;
using FirerusUtilities;
using FirerusUtilities.Extensions;
using UnityEngine;
using UnityEngine.Events;

namespace GameScene.LevelGeneration
{
    public class LevelGenerator : MonoBehaviour
    {
        [Header("Generation Params")]
        [SerializeField] private Vector2Int _size = new(16, 16);
        [SerializeField] private int _maxDecosPerTile = 5;
        [SerializeField] private GameObject[] _decos;
        [SerializeField] private Vector2Int[] _reservedTilesPositions;

        [Header("Perlin Noise")]
        [SerializeField] private Vector2Int _noiseOffset = Vector2Int.zero;
        [SerializeField] private float _noiseScale = 10f;
        [Range(0f, 1f)][SerializeField] private float _waterValue = .2f;
        [Range(0f, 1f)][SerializeField] private float _mountainValue = .7f;

        [Header("References")]
        [SerializeField] private Transform _groundParent;
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

                    if (_reservedTilesPositions.Contains(new Vector2Int(x, y)))
                    {
                        //Instantiate(tile, _groundParent).transform.localPosition = position;
                        continue;
                    }
                    
                    if (pixel <= _waterValue)
                    {
                        tile = _tilesFactory.GetTile(TileType.Water);
                        Instantiate(tile, _groundParent).transform.localPosition = position;
                    }
                    else if (pixel >= _mountainValue)
                    {
                        Instantiate(tile, _groundParent).transform.localPosition = position;
                        tile = _tilesFactory.GetTile(TileType.Mountain);
                        position.y += 1f;
                        Instantiate(tile, _groundParent).transform.localPosition = position;
                    }
                    else
                    {
                        Instantiate(tile, _groundParent).transform.localPosition = position;
                        for (int i = 0; i < Random.Range(0, _maxDecosPerTile); i++)
                        {
                            Vector3 decoPosition = new(Random.Range(-.5f, .5f), 0, Random.Range(-.5f, .5f));
                            Instantiate(_decos.GetRandomElement(), _groundParent).transform.localPosition =
                                position + decoPosition + new Vector3(0, .5f, 0);
                        }
                    }
                }
            }
        }
    }
}

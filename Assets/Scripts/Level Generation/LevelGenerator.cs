using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class LevelGenerator : MonoBehaviour
{
    [Header("Generation Params")]
    [SerializeField] private int _seed = 0;
    [SerializeField] private Vector2Int _size = new Vector2Int(18, 10);

    [Header("Tilemaps")]
    [SerializeField] private Tilemap _groundMap;
    [SerializeField] private Tilemap _decorationMap;
    [SerializeField] private Tilemap _mountainsMap;
    [SerializeField] private BuildingPlacer _buildingPlacer;

    [Header("Factories")]
    [SerializeField] private TilesFacroty _tilesFacroty;

    private Vector2Int _offset;

    public UnityEvent OnLevelFinished;

    private void Start()
    {
        Generate();
    }

    private void Generate()
    {
        if (_size.x < 0 || _size.y < 0)
            throw new System.ArgumentOutOfRangeException("Level size can't be less than 0!");

        _offset = -_size / 2;

        if (_seed == 0)
        {
            _seed = Random.Range(int.MinValue, int.MaxValue);
        }
        Random.InitState(_seed);

        PlaceFloor();
        PlaceWater();
        PlaceMountains();
        PlaceDecoration();
        PlaceCrystal();
        PlaceResources();
        PlaceCreatures();

        OnLevelFinished?.Invoke();
    }

    private void PlaceFloor()
    {
        for (int y = 0; y < _size.y; y++)
        {
            for (int x = 0; x < _size.x; x++)
            {
                Vector3Int position = new Vector3Int(x + _offset.x, y + _offset.y, 0);
                _groundMap.SetTile(position, _tilesFacroty.GetTile(TileType.Ground));
            }
        }
    }

    private void PlaceWater()
    {
        //throw new System.NotImplementedException();
    }
    private void PlaceMountains()
    {
        //throw new System.NotImplementedException();
    }
    private void PlaceDecoration()
    {
        //throw new System.NotImplementedException();
    }
    private void PlaceCrystal()
    {
        //throw new System.NotImplementedException();
    }
    private void PlaceResources()
    {
        //throw new System.NotImplementedException();
    }

    private void PlaceCreatures()
    {
        //throw new System.NotImplementedException();
    }
}

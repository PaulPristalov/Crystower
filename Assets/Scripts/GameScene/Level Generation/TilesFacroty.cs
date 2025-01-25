using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesFacroty : MonoBehaviour
{
    [SerializeField] private TileBase _groundTile;
    [SerializeField] private TileBase _waterTile;
    [SerializeField] private TileBase _mountainTile;
                                 
    public TileBase GetTile(TileType type)
    {
        switch (type)
        {
            case TileType.Ground:
                return _groundTile;
            case TileType.Water:
                return _waterTile;
            case TileType.Mountain:
                return _mountainTile;
            default:
                return null;
        }
    }
}

public enum TileType
{
    Ground,
    Water,
    Mountain
}

using UnityEngine;

namespace GameScene.LevelGeneration
{
    public class TilesFacroty : MonoBehaviour
    {
        [SerializeField] private GameObject _groundTile;
        [SerializeField] private GameObject _waterTile;
        [SerializeField] private GameObject _mountainTile;

        public GameObject GetTile(TileType type)
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
}

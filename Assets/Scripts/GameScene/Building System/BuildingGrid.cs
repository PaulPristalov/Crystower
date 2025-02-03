using System.Collections.Generic;
using UnityEngine;

namespace GameScene.BuildingSystem
{
    /// <summary>
    /// Set the size of available building grid.
    /// Controls free and occupied cells.
    /// Calculates position in the grid.
    /// </summary>
    [RequireComponent(typeof(Grid))]
    public class BuildingGrid : MonoBehaviour
    {
        [SerializeField] private Vector2Int _availableSpaceSize = new(18, 10);
        [SerializeField] private bool _drawGizmos = true;

        private Grid _grid;
        private List<Vector2Int> _occupiedCells = new();

        private void Start()
        {
            _grid = GetComponent<Grid>();
            _occupiedCells.Clear();
        }

        private void OnEnable()
        {
            BuildingGridObject.OnPlaced += ObjectPlaced;
            BuildingGridObject.OnRemoved += ObjectRemoved;
        }

        private void OnDisable()
        {
            BuildingGridObject.OnPlaced -= ObjectPlaced;
            BuildingGridObject.OnRemoved -= ObjectRemoved;
        }

        private void OnDrawGizmos()
        {
            if (!_drawGizmos || _grid == null)
                return;

            Gizmos.color = new Color(0, 0, 1, 0.2f);

            for (int x = -_availableSpaceSize.x / 2; x < _availableSpaceSize.x / 2; x++)
            {
                for (int y = -_availableSpaceSize.y / 2; y < _availableSpaceSize.y / 2; y++)
                {
                    Gizmos.DrawWireCube(_grid.GetCellCenterLocal(new Vector3Int(x, y, 0)),
                        new Vector3(_grid.cellSize.x, 0, _grid.cellSize.y));
                }
            }

            Gizmos.color = new Color(0, 1, 1, 0.4f);
            foreach (var cell in _occupiedCells)
            {
                Gizmos.DrawWireCube(_grid.GetCellCenterLocal((Vector3Int)cell),
                    new Vector3(_grid.cellSize.x, 0, _grid.cellSize.y));
            }
        }

        /// <summary>
        /// Calculates the center point of building in the grid.
        /// </summary>
        /// <param name="size">Size of the building in cells.</param>
        /// <param name="worldPosition">World position of the building.</param>
        /// <returns>World position of building center.</returns>
        public Vector3 CalculateBuildingPosition(Vector2Int size, Vector3 worldPosition)
        {
            float offsetX = (size.x + 1) % 2 * _grid.cellSize.x / 2;
            float offsetY = (size.y + 1) % 2 * _grid.cellSize.y / 2;
            Vector3 offset = new Vector3(offsetX, transform.position.y, offsetY);

            Vector3 cellCenter = _grid.GetCellCenterWorld(_grid.WorldToCell(worldPosition + offset));
            return cellCenter - offset;
        }

        /// <summary>
        /// Returns cells which will be ocupied by building.
        /// </summary>
        /// <param name="size">Size of the building in cells.</param>
        /// <param name="worldPosition">World position of the building.</param>
        /// <returns>List of cells' coordinates in cell cordinates</returns>
        public List<Vector2Int> GetOccupiedCells(Vector2Int size, Vector3 worldPosition)
        {
            Vector3 position = CalculateBuildingPosition(size, worldPosition);
            float offsetX = size.x * _grid.cellSize.x / 2;
            float offsetY = size.y * _grid.cellSize.y / 2;
            Vector3Int startCell = _grid.WorldToCell(position - new Vector3(offsetX, offsetY));

            List<Vector2Int> occupiedCells = new List<Vector2Int>(size.x * size.y);
            for (int x = startCell.x; x < startCell.x + size.x; x++)
            {
                for (int y = startCell.y; y < startCell.y + size.y; y++)
                {
                    occupiedCells.Add(new Vector2Int(x, y));
                }
            }

            return occupiedCells;
        }

        /// <summary>
        /// Check is there any occupied cells.
        /// </summary>
        /// <param name="size">Size of the building in cells.</param>
        /// <param name="worldPosition">World position of the building.</param>
        /// <returns></returns>
        public bool IsCellsAvailable(Vector2Int size, Vector3 worldPosition)
        {
            foreach (var cell in GetOccupiedCells(size, worldPosition))
            {
                if (_occupiedCells.Contains(cell) || Mathf.Abs(cell.x) >= _availableSpaceSize.x / 2 ||
                    Mathf.Abs(cell.y) >= _availableSpaceSize.y / 2)
                    return false;
            }
            return true;
        }

        /// <summary>
        /// Make free cells occupied.
        /// </summary>
        /// <param name="cells"></param>
        public void OccupyCells(List<Vector2Int> cells)
        {
            _occupiedCells.AddRange(cells);
        }

        /// <summary>
        /// Make occupied cells free.
        /// </summary>
        /// <param name="cells"></param>
        public void FreeCells(List<Vector2Int> cells)
        {
            foreach (var cell in cells)
            {
                _occupiedCells.Remove(cell);
            }
        }

        private void ObjectPlaced(BuildingGridObject obj)
        {
            OccupyCells(GetOccupiedCells(obj.Size, obj.transform.position));
        }

        private void ObjectRemoved(BuildingGridObject obj)
        {
            FreeCells(GetOccupiedCells(obj.Size, obj.transform.position));
        }
    }
}

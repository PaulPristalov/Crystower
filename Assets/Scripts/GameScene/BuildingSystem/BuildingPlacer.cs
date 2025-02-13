using GameScene.Player;
using UnityEngine;

namespace GameScene.BuildingSystem
{
    /// <summary>
    /// Creates and place buildings.
    /// </summary>
    public class BuildingPlacer : MonoBehaviour
    {
        [SerializeField] private InputHandler _input;
        [SerializeField] private BuildingGrid _grid;
        [SerializeField] private Building _buildingPrefab;
    
        private BuildingGridObject _currentBuilding;

        private void Start()
        {
            _input.OnFPressed += New;
            _input.OnClicked += PlaceCurrent;
        }

        private void OnDestroy()
        {
            _input.OnFPressed -= New;
            _input.OnClicked -= PlaceCurrent;
        }

        private void Update()
        {
            if (_currentBuilding)
            {
                Move(_currentBuilding, _input.RaycastPosition);
            }
        }

        public void New()
        {
            if (_currentBuilding)
            {
                Destroy(_currentBuilding.gameObject);
            }

            _currentBuilding = Instantiate(_buildingPrefab, transform);
        }

        public void PlaceCurrent()
        {
            if (!_currentBuilding || !_grid.IsCellsAvailable(_currentBuilding.Size, _currentBuilding.transform.position))
                return;
            
            Place(_currentBuilding, _currentBuilding.transform.position);
            _currentBuilding = null;
        }

        /// <summary>
        /// Move the building in the grid position corresponding to the world position.
        /// </summary>
        /// <param name="building"></param>
        /// <param name="worldPosition"></param>
        private void Move(BuildingGridObject building, Vector3 worldPosition)
        {
            Vector3 position = _grid.CalculateBuildingPosition(building.Size, worldPosition);
            building.transform.position = position;
        }

        /// <summary>
        /// Place the building in the grid corresponding to the world position.
        /// </summary>
        /// <param name="building"></param>
        /// <param name="worldPosition"></param>
        /// <exception cref="System.Exception"></exception>
        private void Place(BuildingGridObject building, Vector3 worldPosition)
        {
            //if (!_grid.IsCellsAvailable(building.Size, building.transform.position))
            //    throw new System.Exception($"You can't place building at {building.transform.position}");

            Move(building, worldPosition);
            building.Place();
        }
    }
}

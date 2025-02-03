using GameScene.BuildingSystem;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and place buildings.
/// </summary>
public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingGrid _grid;
    [SerializeField] private BuildingGridObject _buildingPrefab;
    
    private BuildingGridObject _currentBuilding;

    private void Update()
    {
        // Кто должен нести ответственность за передачу Input? InputHandler или сам компонент?
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (_currentBuilding != null)
            {
                Destroy(_currentBuilding.gameObject);
            }

            _currentBuilding = Instantiate(_buildingPrefab, transform);
        }

        if (_currentBuilding != null)
        {
            Move(_currentBuilding, RaycastToMousePosition());

            if (Input.GetMouseButtonDown(0) && 
                _grid.IsCellsAvailable(_currentBuilding.Size, _currentBuilding.transform.position))
            {
                Place(_currentBuilding, _currentBuilding.transform.position);
                _currentBuilding = null;
            }
        }
    }

    /// <summary>
    /// Move the building in the grid position corresponding to the world position.
    /// </summary>
    /// <param name="building"></param>
    /// <param name="worldPosition"></param>
    public void Move(BuildingGridObject building, Vector3 worldPosition)
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
    public void Place(BuildingGridObject building, Vector3 worldPosition)
    {
        if (!_grid.IsCellsAvailable(building.Size, building.transform.position))
            throw new System.Exception($"You can't place building at {building.transform.position}");

        Move(building, worldPosition);
        building.Place();
    }

    private Vector3 RaycastToMousePosition() // В InputHandler
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        return Physics.Raycast(ray, out RaycastHit hit) ? hit.point : Vector3.zero;
    }
}

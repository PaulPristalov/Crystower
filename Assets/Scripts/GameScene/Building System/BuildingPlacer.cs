using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Creates and place buildings.
/// </summary>
public class BuildingPlacer : MonoBehaviour
{
    [SerializeField] private BuildingGrid _grid;
    [SerializeField] private Building _buildingPrefab;
    
    private Building _currentBuilding;
    private List<Building> _buildings;

    private void Start()
    {
        _buildings = new List<Building>();
    }

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
            Move(_currentBuilding, Camera.main.ScreenToWorldPoint(Input.mousePosition));

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
    public void Move(Building building, Vector3 worldPosition)
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
    public void Place(Building building, Vector3 worldPosition)
    {
        if (!_grid.IsCellsAvailable(building.Size, building.transform.position))
            throw new System.Exception($"You can't place building at {building.transform.position}");

        Move(building, worldPosition);
        building.Place();
        _grid.OccupyCells(_grid.GetOccupiedCells(building.Size, building.transform.position));
        _buildings.Add(building);
        building.OnDestroying.AddListener(Destroy);
    }

    /// <summary>
    /// Destroy the building and clear its cells in the grid.
    /// </summary>
    /// <param name="building"></param>
    /// <exception cref="System.NullReferenceException"></exception>
    public void Destroy(Building building)
    {
        if (building == null)
            throw new System.NullReferenceException();

        _grid.FreeCells(_grid.GetOccupiedCells(building.Size, building.transform.position));
        building.OnDestroying.RemoveListener(Destroy);

        if (_buildings.Contains(building))
            _buildings.Remove(building);

        if (building.Placed)
            Destroy(building.gameObject);
    }
}

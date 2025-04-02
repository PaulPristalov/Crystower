using System.Collections.Generic;
using FirerusUtilities.Extensions;
using GameScene.BuildingSystem;
using UnityEngine;

namespace GameScene.ResourceSystem
{
    public class Generator : MonoBehaviour
    {
        [SerializeField] private BuildingGrid _grid;
        [SerializeField] private SourceObject[] _resourcePrefabs;
        [SerializeField] private int _startResourcesCount = 10;
        private List<Vector3> _availablePositions;
        private Vector3 _verticalOffset = Vector3.zero;

        public void Generate()
        {
            _availablePositions = _grid.GetAvailablePositions(new Vector2Int(2, 2));
            for (int i = 0; i < _startResourcesCount; i++)
            {
                Place(_resourcePrefabs.GetRandomElement());
            }
        }

        private void Place(BuildingGridObject resource)
        {
            Vector3 position = _availablePositions.GetRandomElement();
            while (!_grid.IsCellsAvailable(resource.Size, position))
            {
                position = _availablePositions.GetRandomElement();
            }

            var r = Instantiate(resource, transform);
            r.transform.localPosition = position + _verticalOffset;
            r.Place();
        }
    }
}

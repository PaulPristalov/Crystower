using UnityEngine;
using UnityEngine.UI;

namespace GameScene.BuildingSystem
{
    public class GridVisualizer : MonoBehaviour
    {
        [SerializeField] private BuildingGrid _grid;
        [SerializeField] private BuildingPlacer _placer;
        [SerializeField] private Image _cellPrefab;
        [SerializeField] private RectTransform _cellParent;

        private void OnEnable()
        {
            Hide();
            UpdateVisual();
            
            _grid.OnChanged += UpdateVisual;
            _placer.OnStartPlacing += Show;
            _placer.OnFinishPlacing += Hide;
        }

        private void OnDisable()
        {
            _grid.OnChanged -= UpdateVisual;
            _placer.OnStartPlacing -= Show;
            _placer.OnFinishPlacing -= Hide;
        }

        private void UpdateVisual()
        {
            DestroyChildren(_cellParent);
            for (int x = -_grid.AvailableSpaceSize.x / 2; x < _grid.AvailableSpaceSize.x / 2; x++)
            {
                for (int y = -_grid.AvailableSpaceSize.y / 2; y < _grid.AvailableSpaceSize.y / 2; y++)
                {
                    Image image = Instantiate(_cellPrefab, _cellParent);
                    if (_grid.OccupiedCells.Contains(new Vector2Int(x, y)))
                        image.enabled = false;
                }
            }
        }

        private void DestroyChildren(Transform t)
        {
            for (int i = 0; i < t.childCount; i++)
            {
                Destroy(t.GetChild(i).gameObject);
            }
        }

        private void Show()
        {
            _cellParent.gameObject.SetActive(true);
        }

        private void Hide()
        {
            _cellParent.gameObject.SetActive(false);
        }
    }
}

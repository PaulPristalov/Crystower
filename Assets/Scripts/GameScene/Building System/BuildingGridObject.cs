using UnityEngine;
using UnityEngine.Events;

namespace GameScene.BuildingSystem
{
    public class BuildingGridObject : MonoBehaviour
    {
        [SerializeField] private Vector2Int _size = new(2, 2);
        [SerializeField] private bool _autoPlace = true;
        public Vector2Int Size => _size;
        public bool Placed { get; private set; } = false;

        public static event UnityAction<BuildingGridObject> OnPlaced;
        public static event UnityAction<BuildingGridObject> OnRemoved;

        private void Start()
        {
            if (_autoPlace)
            {
                Place();
            }
        }

        public void Place()
        {
            Placed = true;
            OnPlaced?.Invoke(this);
        }

        public void OnDestroy()
        {
            Placed = false;
            OnRemoved?.Invoke(this);
        }
    }
}

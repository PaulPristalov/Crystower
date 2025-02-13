using GameScene.LevelGeneration;
using UnityEngine;
using UnityEngine.Events;

namespace GameScene.BuildingSystem
{
    public class BuildingGridObject : MonoBehaviour
    {
        [field: SerializeField] public Vector2Int Size { get; private set; } = new(2, 2);
        [field: SerializeField] public bool AutoPlace { get; private set; } = true;
        [field: SerializeField] public TileType Type { get; private set; }

        public bool Placed { get; private set; } = false;

        public static event UnityAction<BuildingGridObject> OnPlaced;
        public static event UnityAction<BuildingGridObject> OnRemoved;

        protected virtual void Start()
        {
            if (AutoPlace)
            {
                Place();
            }
        }

        public void Place()
        {
            Placed = true;
            OnPlaced?.Invoke(this);
        }

        protected virtual void OnDestroy()
        {
            Placed = false;
            OnRemoved?.Invoke(this);
        }
    }
}
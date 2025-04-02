using GameScene.BuildingSystem;
using GameScene.HealthSystem;
using UnityEngine;

namespace GameScene.Buildings
{
    [RequireComponent(typeof(Health))]
    public class Building : BuildingGridObject, IClickable
    {
        [field: SerializeField] public string Id { get; private set; }
        [field: SerializeField] public int[] ResourceCost { get; private set; }
        [SerializeField] private int _clicksToBuild = 5;
        
        private Health _health;
        
        public bool Built => _clicksToBuild <= 0;

        protected override void Start()
        {
            base.Start();
            _health = GetComponent<Health>();
        }

        public void Click()
        {
            if (Built || !Placed) return;
            
            _clicksToBuild--;
        }

        private void Update()
        {
            if (!Built) return;
            
            transform.Rotate(Vector3.one);
        }
    }
}

using System;
using FirerusUtilities;
using GameScene.BuildingSystem;
using GameScene.HealthSystem;
using UnityEngine;
using UnityEngine.Events;

namespace GameScene.ResourceSystem
{
    [RequireComponent(typeof(Health))]
    public class SourceObject : BuildingGridObject, IClickable
    {
        [field: SerializeField] public Source Resources { get; private set; }
        
        private Health _health;

        public static event UnityAction<ResourceType, int> OnCollect;

        protected override void Start()
        {
            base.Start();
            _health = GetComponent<Health>();
            _health.OnDying += Collect;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _health.OnDying -= Collect;
        }

        public void Click()
        {
            _health.TakeDamage(1);
        }

        private void Collect()
        {
            OnCollect?.Invoke(Resources.Type, Resources.Cost.GetRandomValue());
            Destroy(gameObject);
        }
        
        [Serializable]
        public class Source
        {
            public ResourceType Type = ResourceType.Wood;
            public IntRange Cost;
        }
    }
}

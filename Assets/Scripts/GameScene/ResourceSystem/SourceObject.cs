using System;
using FirerusUtilities;
using GameScene.BuildingSystem;
using UnityEngine;
using UnityEngine.Events;

namespace GameScene.ResourceSystem
{
    public class SourceObject : BuildingGridObject, IClickable
    {
        [field: SerializeField] public int Health { get; private set; } = 5;
        [field: SerializeField] public Source Resources { get; private set; }

        public static event UnityAction<ResourceType, int> OnCollect;

        public void Click()
        {
            Health--;
            if (Health <= 0)
            {
                OnCollect?.Invoke(Resources.Type, Resources.Cost.GetRandomValue());
                Destroy(gameObject);
            }
        }
        
        [Serializable]
        public class Source
        {
            public ResourceType Type = ResourceType.Wood;
            public IntRange Cost;
        }
    }
}

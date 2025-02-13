using UnityEngine.Events;

namespace GameScene.ResourceSystem
{
    public class Vault
    {
        private readonly int[] _resources;

        public event UnityAction OnCountChanged;
        
        public Vault()
        {
            _resources = new[] { 0, 0, 0, 0, 0 };
            SourceObject.OnCollect += Add;
        }

        public int Get(ResourceType type)
        {
            return _resources[(int)type];
        }

        public void Add(ResourceType type, int count)
        {
            _resources[(int)type] += count;
            OnCountChanged?.Invoke();
        }

        public void Decrease(ResourceType type, int count)
        {
            _resources[(int)type] -= count;
            OnCountChanged?.Invoke();
        }
    }

    public enum ResourceType
    {
        Wood,
        Stone,
        Iron,
        Mana,
        Matter
    }
}

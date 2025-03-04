using System;
using UnityEngine.Events;

namespace GameScene.ResourceSystem
{
    public class Vault
    {
        public int[] Resources { get; private set; }

        public event UnityAction OnCountChanged;
        
        public Vault()
        {
            Resources = new[] { 0, 0, 0, 0, 0 };
            SourceObject.OnCollect += Add;
        }

        public int Get(ResourceType type)
        {
            return Resources[(int)type];
        }

        public void Add(ResourceType type, int count)
        {
            Resources[(int)type] += count;
            OnCountChanged?.Invoke();
        }

        public void Decrease(ResourceType type, int count)
        {
            Resources[(int)type] -= count;
            OnCountChanged?.Invoke();
        }

        public void Decrease(int[] resources)
        {
            if (resources.Length > Resources.Length) throw new ArgumentException();

            for (int i = 0; i < resources.Length; i++)
            {
                Resources[i] -= resources[i];
            }
            OnCountChanged?.Invoke();
        }

        public bool Check(int[] resources)
        {
            for (int i = 0; i < resources.Length; i++)
            {
                if (resources[i] > Resources[i]) return false;
            }

            return true;
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

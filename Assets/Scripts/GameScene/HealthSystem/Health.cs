using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

namespace GameScene.HealthSystem
{
    public class Health : MonoBehaviour
    {
        [field: SerializeField] public int MaxHealth { get; private set; } = 100;
        public int CurrentHealth { get; private set; }
        
        public UnityAction OnDamageTaken { get; set; }
        public UnityAction OnHealed { get; set; }
        public UnityAction OnDying { get; set; }

        private void Awake()
        {
            CurrentHealth = MaxHealth;
        }

        public virtual void ChangeMax(int value)
        {
            if (value < 0)
                throw new ArgumentException("Max health can't be less than 0!");

            MaxHealth = value;
            if (CurrentHealth > MaxHealth)
            {
                TakeDamage(CurrentHealth -  MaxHealth);
            }
        }

        public virtual void TakeDamage(int damage)
        {
            if (damage < 0)
                throw new ArgumentException("Damage can't be less than 0!");

            CurrentHealth -= damage;
            if (CurrentHealth <= 0)
            {
                CurrentHealth = 0;
                Death();
            }

            OnDamageTaken?.Invoke();
        }

        public void Heal() => Heal(MaxHealth);

        public virtual void Heal(int heal)
        {
            if (heal < 0)
                throw new ArgumentException("Heal can't be less than 0!");

            CurrentHealth += heal;
            if (CurrentHealth > MaxHealth)
            {
                CurrentHealth = MaxHealth;
            }

            OnHealed?.Invoke();
        }

        public virtual void Death()
        {
            OnDying?.Invoke();
        }
    }
}

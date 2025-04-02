using System;
using GameScene.HealthSystem;
using UnityEngine;

namespace GameScene.Enemies
{
    [RequireComponent(typeof(Health))]
    public class Enemy : MonoBehaviour
    {
        private Health _health;

        private void Awake()
        {
            _health = GetComponent<Health>();
            _health.OnDying += Death;
        }

        private void OnDestroy()
        {
            _health.OnDying -= Death;
        }

        private void Death()
        {
            Destroy(gameObject);
        }
    }
}

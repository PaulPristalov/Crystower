using GameScene.HealthSystem;
using UnityEngine;

namespace GameScene.Buildings
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody))]
    public class Projectile : MonoBehaviour
    {
        [field: SerializeField] public int Damage { get; private set; } = 10;
        [field: SerializeField] public float Speed { get; private set; } = 10f;

        private Rigidbody _rigidbody;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            Move();
        }

        protected virtual void Move()
        {
            _rigidbody.MovePosition(_rigidbody.position + transform.forward * (Speed * Time.fixedDeltaTime));
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Health health))
            {
                health.TakeDamage(Damage);
                Destroy(gameObject);
            }
        }
    }
}

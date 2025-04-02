using System;
using FirerusUtilities;
using FirerusUtilities.Extensions;
using UnityEngine;

namespace GameScene.Buildings
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _rotatingPart;
        [SerializeField] private Transform _shotPosition;
        
        [SerializeField] private float _targetFindCooldown = 0.5f;
        [SerializeField] private float _cooldown = 1f;
        [SerializeField] private float _attackRadius = 10f;
        [SerializeField] private LayerMask _targetMask = 0;

        private Collider[] _targetColliders;
        private Timer _cooldownTimer;
        private Timer _targetFindCooldownTimer;
        private Transform _target;

        private void Start()
        {
            _cooldownTimer = new Timer(_cooldown);
            _targetFindCooldownTimer = new Timer(_targetFindCooldown);
            _targetFindCooldownTimer.Finished += FindTarget;
            
            StartCoroutine(_cooldownTimer.Start());
            StartCoroutine(_targetFindCooldownTimer.Start());
        }

        private void OnDestroy()
        {
            _targetFindCooldownTimer.Finished -= FindTarget;
        }

        private void Update()
        {
            if (_target && Vector3.Distance(_target.position, transform.position) <= _attackRadius)
            {
                _rotatingPart.LookAt(_target);
                if (_cooldownTimer.CurrentTime <= 0)
                {
                    Instantiate(_projectilePrefab, _shotPosition.position, _shotPosition.rotation);
                    _cooldownTimer.Reset();
                }
            }
        }

        private void FindTarget()
        {
            _targetColliders = Physics.OverlapSphere(transform.position, _attackRadius, _targetMask);
            Collider target = _targetColliders.GetNearest(transform.position);
            if (target) _target = target.transform;
            _targetFindCooldownTimer.Reset();
        }
    }
}

using System;
using Gameplay.Creeps;
using Shared.PauseGame;
using Shared.Pooling;
using UnityEngine;

namespace Gameplay.Turrets.Views
{
    public sealed class ProjectileControllerView : PausableViewBase, ICanPool
    {
        private float _speed;
        private int _damage;
        private float _freezeAmount;
        private float _freezeDuration;
        private Transform _targetTransform;
        private ICreepDamageTaker _targetDamageTaker;
        private IFreezable _targetFreeze;

        private event Action<ProjectileControllerView> OnHitCallback;

        public void Launch(
            Transform target,
            int damage,
            float speed,
            float freezeAmount,
            float freezeDuration,
            Action<ProjectileControllerView> onHitCallback)
        {
            _targetTransform = target;
            _damage = damage;
            _speed = speed;
            _freezeAmount = freezeAmount;
            _freezeDuration = freezeDuration;
            OnHitCallback = onHitCallback;

            target.TryGetComponent(out _targetDamageTaker);
            target.TryGetComponent(out _targetFreeze);
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            _targetTransform = null!;
            _targetDamageTaker = null;
            _targetFreeze = null;
            OnHitCallback = null;

            gameObject.SetActive(false);
        }

        protected override void PausableUpdate()
        {
            if (_targetTransform == null)
            {
                OnHitCallback?.Invoke(this); // Returns to Pool cause target is gone  
                return;
            }

            var currentPosition = transform.position;
            currentPosition.y = 1f;
            var targetPosition = _targetTransform.position;
            targetPosition.y = 1f;

            transform.position = Vector3.MoveTowards(
                currentPosition,
                targetPosition,
                _speed * Time.deltaTime);

            if (Vector3.Distance(currentPosition, targetPosition) < 0.1f)
            {
                Hit();
            }
        }

        private void Hit()
        {
            _targetDamageTaker.TakeDamage(_damage);

            if (_freezeAmount > 0f && _freezeDuration > 0f)
            {
                _targetFreeze.ApplyFreeze(_freezeAmount, _freezeDuration);
            }

            OnHitCallback?.Invoke(this);
        }
    }
}
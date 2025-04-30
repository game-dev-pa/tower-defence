using Gameplay.Creeps.Strategies;
using Shared.PauseGame;
using UnityEngine;

namespace Gameplay.Creeps.Views
{
    public sealed class CreepMovementView : PausableViewBase
    {
        private float _baseSpeed;
        private float _currentSpeed;
        private float _freezeEffectTimer;
        private Vector3 _target;
        private ICreepMovementStrategy _movementStrategy;

        public void Initialize(float movementSpeed, Vector3 target)
        {
            _baseSpeed = movementSpeed;
            _currentSpeed = _baseSpeed;

            _movementStrategy = new DefaultCreepMovementStrategy();
            _target = target;
        }

        public void ApplyFreeze(float freezeAmount, float duration)
        {
            _currentSpeed = _baseSpeed * (1f - freezeAmount);
            _freezeEffectTimer = duration;
        }

        public bool IsNearTarget(float threshold)
        {
            return Vector3.Distance(transform.position, _target) <= threshold;
        }

        public void ResetState()
        {
            _currentSpeed = _baseSpeed;
            _freezeEffectTimer = 0f;
        }

        protected override void PausableUpdate()
        {
            _movementStrategy.Move(transform, _target, _currentSpeed);

            if (!(_freezeEffectTimer > 0f))
                return;

            _freezeEffectTimer -= Time.deltaTime;
            if (_freezeEffectTimer <= 0f)
            {
                _currentSpeed = _baseSpeed;
            }
        }
    }
}
#nullable enable
using System;
using Config;
using Gameplay.Creeps.Views;
using Gameplay.Turrets.Services;
using Shared.PauseGame;
using UnityEngine;
using VContainer;

namespace Gameplay.Turrets.Views
{
    public sealed class TurretControllerView : PausableViewBase
    {
        private const int MAX_TARGETS = 5;
        private static readonly Collider[] Colliders = new Collider[MAX_TARGETS];

        [SerializeField] private TurretData? _turretData;

        private float _fireCooldown;
        private float _timeSinceLastShot;
        private IShootStrategy _shootStrategy = null!;
        private IShootStrategyFactory _shootStrategyFactory = null!;

        [Inject]
        public void Construct(IShootStrategyFactory shootStrategyFactory)
        {
            _shootStrategyFactory = shootStrategyFactory;
        }

        protected override void PausableUpdate()
        {
            _timeSinceLastShot += Time.deltaTime;
            var target = FindTargetInRange();

            if (target == null || !(_timeSinceLastShot >= _fireCooldown))
                return;

            _shootStrategy?.Shoot(transform, target);
            _timeSinceLastShot = 0f;
        }

        private void Start()
        {
            if (_turretData == null)
                throw new NullReferenceException("TurretData NOT assigned!!");

            _fireCooldown = 1f / _turretData.FireRate;
            _shootStrategy = _shootStrategyFactory.CreateStrategy(_turretData);
        }

        private CreepDamageTakerView? FindTargetInRange()
        {
            var hitCount = Physics.OverlapSphereNonAlloc(transform.position, _turretData!.ImpactRadius, Colliders);
            for (var i = 0; i < hitCount; i++)
            {
                if (Colliders[i].TryGetComponent<CreepDamageTakerView>(out var target))
                {
                    return target;
                }
            }

            return null;
        }

        private void OnDrawGizmosSelected()
        {
            if (_turretData == null)
                return;

            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _turretData.ImpactRadius);
        }
    }
}
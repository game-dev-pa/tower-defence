using Gameplay.Creeps;
using Shared;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public sealed class FreezeShootStrategy : IShootStrategy
    {
        private readonly IProjectilePoolService _projectilePoolService;
        private readonly float _projectileSpeed;
        private readonly int _damage;
        private readonly float _freezeAmount;
        private readonly float _freezeDuration;

        public FreezeShootStrategy(
            IProjectilePoolService projectilePoolService,
            float projectileSpeed,
            int damage,
            float freezeAmount,
            float freezeDuration)
        {
            _projectilePoolService = projectilePoolService;
            _projectileSpeed = projectileSpeed;
            _damage = damage;
            _freezeAmount = freezeAmount;
            _freezeDuration = freezeDuration;
        }

        public void Shoot<T>(Transform origin, T target) where T : ICreepDamageTaker, ITargetable
        {
            _projectilePoolService.Spawn(
                origin.position,
                target.Transform,
                _damage,
                _projectileSpeed,
                _freezeAmount,
                _freezeDuration
            );
        }
    }
}
using Gameplay.Creeps;
using Shared;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public sealed class RegularShootStrategy : IShootStrategy
    {
        private readonly IProjectilePoolService _projectilePoolService;
        private readonly float _projectileSpeed;
        private readonly int _damage;

        public RegularShootStrategy(IProjectilePoolService projectilePoolService, float projectileSpeed, int damage)
        {
            _projectilePoolService = projectilePoolService;
            _projectileSpeed = projectileSpeed;
            _damage = damage;
        }

        public void Shoot<T>(Transform origin, T target) where T : ICreepDamageTaker, ITargetable
        {
            _projectilePoolService.Spawn(
                origin.position,
                target.Transform,
                _damage,
                _projectileSpeed,
                0f,
                0f
            );
        }
    }
}
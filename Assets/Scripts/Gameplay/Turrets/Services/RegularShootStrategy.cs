using Gameplay.Creeps;
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

        public void Shoot(Transform origin, ICreepDamageTaker target)
        {
            _projectilePoolService.Spawn(
                origin.position,
                ((MonoBehaviour)target).transform,
                _damage,
                _projectileSpeed,
                0f,
                0f
            );
        }
    }
}
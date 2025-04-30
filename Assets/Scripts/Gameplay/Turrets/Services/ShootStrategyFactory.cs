using Config;
using Gameplay.Turrets.Data;
using JetBrains.Annotations;

namespace Gameplay.Turrets.Services
{
    [UsedImplicitly]
    public sealed class ShootStrategyFactory : IShootStrategyFactory
    {
        private readonly IProjectilePoolService _projectilePoolService;

        public ShootStrategyFactory(IProjectilePoolService projectilePoolService)
        {
            _projectilePoolService = projectilePoolService;
        }

        public IShootStrategy CreateStrategy(TurretData turretData)
        {
            return turretData.Type switch
            {
                TurretType.Regular => new RegularShootStrategy(
                    _projectilePoolService,
                    turretData.ProjectileSpeed,
                    turretData.Damage
                ),

                TurretType.Freezing => new FreezeShootStrategy(
                    _projectilePoolService,
                    turretData.ProjectileSpeed,
                    turretData.Damage,
                    turretData.FreezeAmount,
                    turretData.FreezeDuration
                ),

                _ => new RegularShootStrategy(
                    _projectilePoolService,
                    turretData.ProjectileSpeed,
                    turretData.Damage)
            };
        }
    }
}
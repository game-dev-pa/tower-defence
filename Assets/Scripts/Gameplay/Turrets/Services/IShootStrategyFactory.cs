using Config;

namespace Gameplay.Turrets.Services
{
    public interface IShootStrategyFactory
    {
        IShootStrategy CreateStrategy(TurretData turretData);
    }
}
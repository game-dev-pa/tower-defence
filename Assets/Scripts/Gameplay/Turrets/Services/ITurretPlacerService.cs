using Config;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public interface ITurretPlacerService
    {
        bool TryPlaceTurret(TurretData turretData, Vector3 position);
    }
}
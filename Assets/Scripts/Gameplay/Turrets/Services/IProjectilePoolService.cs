using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public interface IProjectilePoolService
    {
        void Spawn(
            Vector3 position,
            Transform target,
            int damage,
            float speed,
            float freezeAmount,
            float freezeDuration);
    }
}
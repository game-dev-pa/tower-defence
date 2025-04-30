using UnityEngine;

namespace Gameplay.Creeps.Strategies
{
    public interface ICreepMovementStrategy
    {
        void Move(Transform transform, Vector3 targetPosition, float speed);
    }
}
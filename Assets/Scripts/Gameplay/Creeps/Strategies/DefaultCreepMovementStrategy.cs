using UnityEngine;

namespace Gameplay.Creeps.Strategies
{
    public sealed class DefaultCreepMovementStrategy : ICreepMovementStrategy
    {
        public void Move(Transform transform, Vector3 targetPosition, float speed)
        {
            var position = transform.position;
            var direction = (targetPosition - position).normalized;
            position += direction * (speed * Time.deltaTime);
            transform.position = position;
        }
    }
}
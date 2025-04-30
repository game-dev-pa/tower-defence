using Gameplay.Creeps;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public interface IShootStrategy
    {
        void Shoot(Transform origin, ICreepDamageTaker target);
    }
}
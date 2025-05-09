using Gameplay.Creeps;
using Shared;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public interface IShootStrategy
    {
        void Shoot<T>(Transform origin, T target) where T : ICreepDamageTaker, ITargetable;
    }
}
using Config;
using UnityEngine;

namespace Gameplay.Creeps.Services
{
    public interface ICreepPoolService
    {
        void Spawn(CreepData creepData, Vector3 spawnPosition);
    }
}
using System;

namespace Gameplay.Creeps.Services
{
    public interface ICreepTrackerService
    {
        event Action OnAllCreepsDead;
    }
}
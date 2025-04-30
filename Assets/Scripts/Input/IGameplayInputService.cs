using System;

namespace Input
{
    public interface IGameplayInputService : IDisposable
    {
        event Action OnTurretPlaced;
    }
}
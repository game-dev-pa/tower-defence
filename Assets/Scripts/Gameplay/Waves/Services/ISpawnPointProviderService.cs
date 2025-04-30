using System.Collections.Generic;
using Gameplay.Waves.Views;

namespace Gameplay.Waves.Services
{
    public interface ISpawnPointProviderService
    {
        IReadOnlyList<SpawnPointView> GetSpawnPoints();
    }
}
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Config;
using Gameplay.Waves.Views;

namespace Gameplay.Waves.Services
{
    public interface IWaveSpawnerService
    {
        Task SpawnWaveAsync(
            IReadOnlyList<SpawnPointView> spawnPoints,
            WaveData.WaveEntry waveData,
            CancellationToken cancellationToken = default);
    }
}
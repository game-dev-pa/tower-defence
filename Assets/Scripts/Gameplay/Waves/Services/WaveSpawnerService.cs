using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Config;
using Gameplay.Creeps.Services;
using Gameplay.Waves.Views;
using JetBrains.Annotations;
using Shared.Extensions;
using UnityEngine;

namespace Gameplay.Waves.Services
{
    [UsedImplicitly]
    public sealed class WaveSpawnerService : IWaveSpawnerService
    {
        private readonly ICreepPoolService _creepPoolService;

        public WaveSpawnerService(ICreepPoolService creepPoolService)
        {
            _creepPoolService = creepPoolService;
        }

        public async Task SpawnWaveAsync(
            IReadOnlyList<SpawnPointView> spawnPoints,
            WaveData.WaveEntry waveData,
            CancellationToken cancellationToken)
        {
            for (var i = 0; i < waveData.Count; i++)
            {
                var randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                _creepPoolService.Spawn(waveData.CreepData, randomSpawnPoint.Position);

                var delay = waveData.SpawnInterval.ToMilliseconds();
                await Task.Delay(delay, cancellationToken);
            }
        }
    }
}
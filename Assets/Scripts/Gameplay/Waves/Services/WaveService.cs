#nullable enable
using System;
using System.Threading.Tasks;
using Config;
using JetBrains.Annotations;
using Shared.Utilities;
using UnityEngine;

namespace Gameplay.Waves.Services
{
    [UsedImplicitly]
    public sealed class WaveService : IWaveService
    {
        private readonly WaveData _waveData;
        private readonly ISpawnPointProviderService _spawnPointProviderService;
        private readonly IWaveSpawnerService _waveSpawnerService;
        private readonly ICancellationTokenManager _cancellationTokenManager;

        public event Action? OnWaveStarted;
        public event Action? OnWaveCompleted;

        public WaveService(
            WaveData waveData,
            ISpawnPointProviderService spawnPointProviderService,
            IWaveSpawnerService waveSpawnerService,
            ICancellationTokenManager cancellationTokenManager)
        {
            _waveData = waveData;
            _spawnPointProviderService = spawnPointProviderService;
            _waveSpawnerService = waveSpawnerService;
            _cancellationTokenManager = cancellationTokenManager;
        }

        public async Task StartWavesAsync()
        {
            StopWaves();

            try
            {
                _cancellationTokenManager.StartNewCancellationToken();

                foreach (var currentWaveData in _waveData.Waves)
                {
                    OnWaveStarted?.Invoke();
                    await _waveSpawnerService.SpawnWaveAsync(
                        _spawnPointProviderService.GetSpawnPoints(),
                        currentWaveData,
                        _cancellationTokenManager.Token);
                    OnWaveCompleted?.Invoke();
                }
            }
            catch (TaskCanceledException)
            {
                Debug.Log("Wave spawning was canceled.");
            }
        }

        public void StopWaves()
        {
            _cancellationTokenManager.Cancel();
        }
    }
}
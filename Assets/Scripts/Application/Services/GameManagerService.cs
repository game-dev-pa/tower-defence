#nullable enable
using System;
using Config;
using Gameplay.Base;
using Gameplay.Creeps.Services;
using Gameplay.Waves.Services;
using JetBrains.Annotations;
using Shared.PauseGame;
using Shared.Utilities;
using VContainer.Unity;

namespace Application.Services
{
    [UsedImplicitly]
    public sealed class GameManagerService : IInitializable, IGameManagerService
    {
        private readonly GameConfigData _gameConfigData;
        private readonly GameStateData _gameStateData;
        private readonly IBaseControllerView _baseControllerView;
        private readonly IPausableService _pausableService;
        private readonly IWaveService _waveService;
        private readonly ICreepTrackerService _creepTrackerService;
        private readonly ICancellationTokenManager _cancellationTokenManager;

        public int CurrentCoins => _gameStateData.Coins;

        public event Action? OnGameLose;
        public event Action<int>? OnCoinChanged;
        public event Action? OnGameWin;

        public GameManagerService(
            WaveData wavesData,
            GameConfigData gameConfigData,
            IBaseControllerView baseControllerView,
            IPausableService pausableService,
            IWaveService waveService,
            ICreepTrackerService creepTrackerService,
            ICancellationTokenManager cancellationTokenManager)
        {
            _gameConfigData = gameConfigData;
            _baseControllerView = baseControllerView;
            _pausableService = pausableService;
            _waveService = waveService;
            _creepTrackerService = creepTrackerService;
            _cancellationTokenManager = cancellationTokenManager;

            _gameStateData = new GameStateData(_gameConfigData.StartingCoins, wavesData.Waves.Count);

            _gameStateData.OnCoinChanged += HandleCoinValueChanged;
            _gameStateData.OnGameWin += HandleGameWon;
            _baseControllerView.OnBaseDestroyedCallback += HandleGameLose;
            _waveService.OnWaveStarted += HandleWaveStarted;
            _waveService.OnWaveCompleted += HandleWaveCompleted;
            _creepTrackerService.OnAllCreepsDead += HandleAllCreepsDead;
        }

        public void Initialize()
        {
            _cancellationTokenManager.StartNewCancellationToken();

            OnCoinChanged?.Invoke(CurrentCoins);
            _baseControllerView.Initialize(_gameConfigData.BaseHealth);
            _waveService.StartWavesAsync();
        }

        void IDisposable.Dispose()
        {
            _gameStateData.OnCoinChanged -= HandleCoinValueChanged;
            _gameStateData.OnGameWin -= HandleGameWon;
            _baseControllerView.OnBaseDestroyedCallback -= HandleGameLose;
            _waveService.OnWaveStarted -= HandleWaveStarted;
            _waveService.OnWaveCompleted -= HandleWaveCompleted;
            _creepTrackerService.OnAllCreepsDead -= HandleAllCreepsDead;
        }

        public void AddCoins(int amount)
        {
            _gameStateData.AddCoins(amount);
        }

        public bool RemoveCoins(int amount)
        {
            return _gameStateData.SpendCoins(amount);
        }

        private void HandleGameLose()
        {
            OnGameLose?.Invoke();
            Stop();
        }

        private void HandleGameWon()
        {
            OnGameWin?.Invoke();
            Stop();
        }

        private void HandleCoinValueChanged(int coins)
        {
            OnCoinChanged?.Invoke(coins);
        }

        private void HandleWaveStarted()
        {
            _gameStateData.StartNextWave();
        }

        private void HandleWaveCompleted()
        {
            _gameStateData.CompleteCurrentWave();
        }

        private void HandleAllCreepsDead()
        {
            _gameStateData.AllCreepsDied();
        }

        private void Stop()
        {
            _waveService.StopWaves();
            _cancellationTokenManager.Cancel();

            _pausableService.PauseAll();
        }
    }
}
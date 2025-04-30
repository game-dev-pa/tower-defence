#nullable enable
using System;

namespace Config
{
    internal sealed class GameStateData
    {
        private bool _allCreepsDead;
        private bool _gameHasEnded;
        private bool _waveIsMarkedCompleted;

        public int Coins { get; private set; }
        private int CurrentWave { get; set; }
        private int TotalWaves { get; }
        private bool AllWavesCompleted => CurrentWave >= TotalWaves - 1 && _waveIsMarkedCompleted;

        public event Action<int>? OnCoinChanged;
        public event Action? OnGameWin;

        public GameStateData(int startingCoins, int totalWaves)
        {
            Coins = startingCoins;
            TotalWaves = totalWaves;
            CurrentWave = -1;
        }

        public void AddCoins(int amount)
        {
            if (amount <= 0) return;
            Coins += amount;
            OnCoinChanged?.Invoke(Coins);
        }

        public bool SpendCoins(int amount)
        {
            if (amount <= 0 || amount > Coins) return false;
            Coins -= amount;
            OnCoinChanged?.Invoke(Coins);
            return true;
        }

        public void StartNextWave()
        {
            if (AllWavesCompleted)
            {
                return;
            }

            CurrentWave++;
            _allCreepsDead = false;
        }

        public void CompleteCurrentWave()
        {
            _waveIsMarkedCompleted = true;
            if (AllWavesCompleted)
            {
                CheckWinCondition();
            }
        }

        public void AllCreepsDied()
        {
            _allCreepsDead = true;
            CheckWinCondition();
        }

        private void CheckWinCondition()
        {
            if (_gameHasEnded)
                return;

            if (!_allCreepsDead || !AllWavesCompleted)
                return;

            _gameHasEnded = true;

            OnGameWin?.Invoke();
        }
    }
}
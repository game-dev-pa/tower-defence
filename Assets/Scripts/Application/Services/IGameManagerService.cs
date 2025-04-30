using System;

namespace Application.Services
{
    public interface IGameManagerService : IDisposable
    {
        int CurrentCoins { get; }

        event Action OnGameLose;
        event Action<int> OnCoinChanged;
        event Action OnGameWin;

        void AddCoins(int coins);
        bool RemoveCoins(int coins);
    }
}
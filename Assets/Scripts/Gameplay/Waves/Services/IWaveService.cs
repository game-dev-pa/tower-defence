using System;
using System.Threading.Tasks;

namespace Gameplay.Waves.Services
{
    public interface IWaveService
    {
        event Action OnWaveStarted;
        event Action OnWaveCompleted;

        Task StartWavesAsync();
        void StopWaves();
    }
}
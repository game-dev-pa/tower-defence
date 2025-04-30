using System.Collections.Generic;
using Gameplay.Waves.Services;
using UnityEngine;

namespace Gameplay.Waves.Views
{
    public sealed class SpawnPointManagerView : MonoBehaviour, ISpawnPointProviderService
    {
        [SerializeField] private SpawnPointView[] _spawnPoints;

        public IReadOnlyList<SpawnPointView> GetSpawnPoints()
        {
            return _spawnPoints;
        }
    }
}
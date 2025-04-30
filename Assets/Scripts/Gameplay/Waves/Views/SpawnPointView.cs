using UnityEngine;

namespace Gameplay.Waves.Views
{
    public sealed class SpawnPointView : MonoBehaviour
    {
        [SerializeField] private Transform _spawnLocation;

        public Vector3 Position => _spawnLocation != null ? _spawnLocation.position : transform.position;
    }
}
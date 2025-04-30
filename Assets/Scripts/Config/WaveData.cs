using System;
using System.Collections.Generic;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "CreateModels/" + nameof(WaveData))]
    public sealed class WaveData : ScriptableObject
    {
        [field: SerializeField] public List<WaveEntry> Waves { get; private set; } = new List<WaveEntry>();

        [Serializable]
        public sealed class WaveEntry
        {
            [field: SerializeField] public CreepData CreepData { get; private set; }
            [field: SerializeField] public int Count { get; private set; } = 1;
            [field: SerializeField] public float SpawnInterval { get; private set; } = 1f;

#if UNITY_EDITOR
            private void OnValidate()
            {
                Count = Mathf.Max(1, Count);
                SpawnInterval = Mathf.Max(0.1f, SpawnInterval);
            }
#endif
        }
    }
}
using Gameplay.Creeps.Data;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "CreateModels/" + nameof(CreepData))]
    public sealed class CreepData : ScriptableObject
    {
        [field: SerializeField] public CreepType Type { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float MovementSpeed { get; private set; }
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int RewardCoins { get; private set; }
        [field: SerializeField] public int DamageToBase { get; private set; }


#if UNITY_EDITOR
        private void OnValidate()
        {
            MovementSpeed = Mathf.Max(0.1f, MovementSpeed);
            MaxHealth = Mathf.Max(1, MaxHealth);
            RewardCoins = Mathf.Max(0, RewardCoins);
            DamageToBase = Mathf.Max(0, DamageToBase);
        }
#endif
    }
}
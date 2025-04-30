using Gameplay.Turrets.Data;
using UnityEngine;

namespace Config
{
    [CreateAssetMenu(menuName = "CreateModels/" + nameof(TurretData))]
    public sealed class TurretData : ScriptableObject
    {
        [field: SerializeField] public TurretType Type { get; private set; }
        [field: SerializeField] public GameObject Prefab { get; private set; }
        [field: SerializeField] public float ProjectileSpeed { get; private set; }
        [field: SerializeField] public float FireRate { get; private set; }
        [field: SerializeField] public float ImpactRadius { get; private set; }
        [field: SerializeField] public int Damage { get; private set; }
        [field: SerializeField] public int Cost { get; private set; }

        [field: Range(0f, 1f)]
        [field: SerializeField]
        public float FreezeAmount { get; private set; }

        [field: SerializeField] public float FreezeDuration { get; private set; }

#if UNITY_EDITOR
        private void OnValidate()
        {
            ProjectileSpeed = Mathf.Max(0.1f, ProjectileSpeed);
            FireRate = Mathf.Max(0.1f, FireRate);
            ImpactRadius = Mathf.Max(0.1f, ImpactRadius);
            Damage = Mathf.Max(0, Damage);
            Cost = Mathf.Max(0, Cost);
            FreezeAmount = Mathf.Max(0, FreezeAmount);
            FreezeDuration = Mathf.Max(0, FreezeDuration);
        }
#endif
    }
}
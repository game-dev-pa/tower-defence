using System;
using Shared;
using UnityEngine;

namespace Gameplay.Creeps.Views
{
    public sealed class CreepDamageTakerView : MonoBehaviour, ICreepDamageTaker, IFreezable, ITargetable
    {
        public Transform Transform => transform;

        public event Action<float> OnTakeDamage;
        public event Action<float, float> OnApplyFreeze;

        public void TakeDamage(float damage)
        {
            OnTakeDamage?.Invoke(damage);
        }

        public void ApplyFreeze(float freezeAmount, float duration)
        {
            OnApplyFreeze?.Invoke(freezeAmount, duration);
        }
    }
}
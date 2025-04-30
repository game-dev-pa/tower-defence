using System;
using Shared.HealthBar;
using UnityEngine;

namespace Gameplay.Creeps.Views
{
    public sealed class CreepHealthView : MonoBehaviour, IHealth
    {
        private float _currentHealth;

        public float CurrentHealth
        {
            get => _currentHealth;
            private set
            {
                _currentHealth = value;
                OnHealthChanged?.Invoke(CurrentHealth, MaxHealth);
            }
        }

        public float MaxHealth { get; private set; }

        public event Action<float, float> OnHealthChanged;

        public void Initialize(float maxHealth)
        {
            MaxHealth = maxHealth;

            CurrentHealth = MaxHealth;
        }

        public void ApplyDamage(float damage)
        {
            CurrentHealth -= damage;

            CurrentHealth = Mathf.Max(CurrentHealth, 0);
        }

        public void ResetState()
        {
            CurrentHealth = MaxHealth;
        }
    }
}
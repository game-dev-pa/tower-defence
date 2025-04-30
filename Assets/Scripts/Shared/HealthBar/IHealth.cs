using System;

namespace Shared.HealthBar
{
    public interface IHealth
    {
        float CurrentHealth { get; }
        float MaxHealth { get; }

        event Action<float, float> OnHealthChanged;
    }
}
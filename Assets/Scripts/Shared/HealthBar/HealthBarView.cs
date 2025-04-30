using System;
using UnityEngine;
using UnityEngine.UI;

namespace Shared.HealthBar
{
    public sealed class HealthBarView : MonoBehaviour
    {
        [SerializeField] private Image _healthFillImage;

        private IHealth _health;

        private void Awake()
        {
            _health = GetComponentInParent<IHealth>();
            if (_health == null)
                throw new NullReferenceException("IHealth NOT FOUND in parent objects!!");

            _health.OnHealthChanged += UpdateHealthBar;
            UpdateHealthBar(_health.CurrentHealth, _health.MaxHealth);
        }

        private void OnDestroy()
        {
            _health.OnHealthChanged -= UpdateHealthBar;
        }

        private void UpdateHealthBar(float currentHealth, float maxHealth)
        {
            _healthFillImage.fillAmount = currentHealth / maxHealth;
        }
    }
}
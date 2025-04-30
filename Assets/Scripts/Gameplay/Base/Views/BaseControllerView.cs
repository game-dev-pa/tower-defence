using System;
using UnityEngine;

namespace Gameplay.Base.Views
{
    [RequireComponent(typeof(BaseHealthView), typeof(SphereCollider))]
    public sealed class BaseControllerView : MonoBehaviour, IBaseControllerView, IBaseDamageTaker
    {
        private BaseHealthView _baseHealthView;
        private SphereCollider _sphereCollider;

        public Transform Transform => transform;
        public float BaseDistanceDamageThreshold => _sphereCollider.radius * transform.localScale.x;

        public event Action OnBaseDestroyedCallback;

        public void Initialize(int maxBaseHealth)
        {
            _baseHealthView = GetComponent<BaseHealthView>();
            _sphereCollider = GetComponent<SphereCollider>();

            _baseHealthView.Initialize(maxBaseHealth);
        }

        public void TakeDamage(float damage)
        {
            _baseHealthView.ApplyDamage(damage);
            if (!(_baseHealthView.CurrentHealth <= 0f))
                return;

            OnBaseDestroyedCallback?.Invoke();
        }
    }
}
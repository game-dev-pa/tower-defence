using System;
using Application.Services;
using Config;
using Gameplay.Base;
using Shared.PauseGame;
using Shared.Pooling;
using UnityEngine;
using VContainer;

namespace Gameplay.Creeps.Views
{
    [RequireComponent(
        typeof(CreepHealthView),
        typeof(CreepMovementView),
        typeof(CreepDamageTakerView))]
    public sealed class CreepControllerView : PausableViewBase, ICanPool
    {
        private CreepHealthView _creepHealthView;
        private CreepMovementView _creepMovementView;
        private CreepDamageTakerView _creepDamageTakerView;

        private CreepData _creepData;
        private IBaseControllerView _baseControllerView;
        private IBaseDamageTaker _baseDamageTaker;
        private IGameManagerService _gameManagerService;

        private event Action<CreepControllerView> OnDiedCallback;

        [Inject]
        public void Construct(
            IBaseControllerView baseControllerView,
            IBaseDamageTaker baseDamageTaker,
            IGameManagerService gameManagerService)
        {
            _baseControllerView = baseControllerView;
            _baseDamageTaker = baseDamageTaker;
            _gameManagerService = gameManagerService;
        }

        public void Initialize(CreepData creepData, Action<CreepControllerView> onDiedCallback)
        {
            _creepData = creepData;
            _creepHealthView.Initialize(_creepData.MaxHealth);
            _creepMovementView.Initialize(_creepData.MovementSpeed, _baseControllerView.Transform.position);
            OnDiedCallback = onDiedCallback;
        }

        public void OnGetFromPool()
        {
            gameObject.SetActive(true);
        }

        public void OnReturnToPool()
        {
            _creepData = null;
            _creepMovementView.ResetState();
            _creepHealthView.ResetState();
            OnDiedCallback = null;

            gameObject.SetActive(false);
        }

        protected override void PausableUpdate()
        {
            if (_creepMovementView.IsNearTarget(_baseControllerView.BaseDistanceDamageThreshold))
            {
                DamageBase();
            }
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            _creepDamageTakerView.OnTakeDamage -= HandleDamageTakerView;
            _creepDamageTakerView.OnApplyFreeze -= HandleFreeze;
        }

        private void Awake()
        {
            _creepHealthView = GetComponent<CreepHealthView>();
            _creepHealthView.OnHealthChanged += HandleHealthViewChanged;
            _creepMovementView = GetComponent<CreepMovementView>();
            _creepDamageTakerView = GetComponent<CreepDamageTakerView>();
            _creepDamageTakerView.OnTakeDamage += HandleDamageTakerView;
            _creepDamageTakerView.OnApplyFreeze += HandleFreeze;
        }

        private void HandleHealthViewChanged(float currentHealth, float _)
        {
            if (!(currentHealth <= 0f))
                return;

            _gameManagerService.AddCoins(_creepData.RewardCoins);
            OnDiedCallback?.Invoke(this);
        }

        private void HandleDamageTakerView(float damage)
        {
            _creepHealthView.ApplyDamage(damage);
        }

        private void HandleFreeze(float freezeAmount, float duration)
        {
            _creepMovementView.ApplyFreeze(freezeAmount, duration);
        }

        private void DamageBase()
        {
            _baseDamageTaker.TakeDamage(_creepData.DamageToBase);
            OnDiedCallback?.Invoke(this);
        }
    }
}
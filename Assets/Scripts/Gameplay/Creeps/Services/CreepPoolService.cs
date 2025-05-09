using System;
using System.Collections.Generic;
using Config;
using Gameplay.Creeps.Data;
using Gameplay.Creeps.Views;
using JetBrains.Annotations;
using Shared.Pooling;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace Gameplay.Creeps.Services
{
    [UsedImplicitly]
    public class CreepPoolService : ICreepPoolService, ICreepTrackerService
    {
        private readonly Transform _parentHolder;
        private readonly IObjectResolver _objectResolver;

        private readonly Dictionary<CreepType, ObjectPool<CreepControllerView>> _allCreepPools =
            new Dictionary<CreepType, ObjectPool<CreepControllerView>>();

        private readonly Dictionary<CreepControllerView, CreepData> _activeCreeps =
            new Dictionary<CreepControllerView, CreepData>();

        public event Action OnAllCreepsDead;

        public CreepPoolService(Transform parentHolder, IObjectResolver objectResolver)
        {
            _parentHolder = parentHolder;
            _objectResolver = objectResolver;
        }

        public void Spawn(CreepData creepData, Vector3 spawnPosition)
        {
            if (!_allCreepPools.TryGetValue(creepData.Type, out var creepPool))
            {
                creepPool = CreatePool(creepData);
                _allCreepPools[creepData.Type] = creepPool;
            }

            var creep = creepPool.GetFromPool();
            creep.transform.position = spawnPosition;

            creep.Initialize(creepData, OnDiedCallback);

            _activeCreeps[creep] = creepData;
        }

        private ObjectPool<CreepControllerView> CreatePool(CreepData data)
        {
            return new ObjectPool<CreepControllerView>(() => CreateCreep(data));
        }

        private CreepControllerView CreateCreep(CreepData creepData)
        {
            var creep = Object.Instantiate(creepData.Prefab, _parentHolder, false);
            _objectResolver.InjectGameObject(creep);
            creep.SetActive(false);
            var creepController = creep.GetComponent<CreepControllerView>();
            return creepController;
        }

        private void OnDiedCallback(CreepControllerView creepControllerView)
        {
            if (!_activeCreeps.TryGetValue(creepControllerView, out var creepData))
                return;

            _allCreepPools[creepData.Type].ReturnToPool(creepControllerView);
            _activeCreeps.Remove(creepControllerView);

            if (_activeCreeps.Count == 0)
            {
                OnAllCreepsDead?.Invoke();
            }
        }
    }
}
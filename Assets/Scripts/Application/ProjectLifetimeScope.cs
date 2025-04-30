using Application.Services;
using Camera;
using Config;
using Gameplay.Base.Views;
using Gameplay.Creeps.Services;
using Gameplay.Turrets.Services;
using Gameplay.Turrets.Views;
using Gameplay.Waves.Services;
using Gameplay.Waves.Views;
using Input;
using Shared.PauseGame;
using Shared.Utilities;
using UI.Views;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Application
{
    public sealed class ProjectLifetimeScope : LifetimeScope
    {
        [SerializeField] private GameConfigData _gameConfigData;
        [SerializeField] private WaveData _waveData;
        [SerializeField] private Transform _creepParent;
        [SerializeField] private Transform _turretParent;
        [SerializeField] private Transform _projectileParent;

        private ICancellationTokenManager _cancellationTokenManager;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<GameManagerService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PausableService>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlayerActions>(Lifetime.Singleton).AsSelf().AsImplementedInterfaces();

            builder.Register<ICancellationTokenManager, CancellationTokenManager>(Lifetime.Singleton);
            builder.Register<IWaveSpawnerService, WaveSpawnerService>(Lifetime.Singleton);
            builder.Register<IWaveService, WaveService>(Lifetime.Singleton);
            builder.Register<ICameraProviderService, CameraProviderService>(Lifetime.Singleton);
            builder.Register<IGameplayInputService, GameplayInputService>(Lifetime.Singleton);
            builder.Register<IShootStrategyFactory, ShootStrategyFactory>(Lifetime.Singleton);
            builder.Register<CreepPoolService>(Lifetime.Singleton)
                .As<ICreepPoolService, ICreepTrackerService>()
                .WithParameter(typeof(Transform), _creepParent);
            builder.Register<ITurretPlacerService>(
                objectResolver => new TurretPlacerService(_turretParent, objectResolver),
                Lifetime.Singleton);
            builder.Register<IProjectilePoolService>(
                objectResolver => new ProjectilePoolService(_gameConfigData.ProjectilePrefab, _projectileParent),
                Lifetime.Singleton);

            builder.RegisterComponentInHierarchy<BaseControllerView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<UIManagerView>();
            builder.RegisterComponentInHierarchy<SpawnPointManagerView>().AsImplementedInterfaces();
            builder.RegisterComponentInHierarchy<TurretPlacerManagerView>();

            builder.RegisterInstance(_gameConfigData);
            builder.RegisterInstance(_waveData);
        }

        private void Start()
        {
            _cancellationTokenManager = Container.Resolve<ICancellationTokenManager>();
        }

        private void OnApplicationQuit()
        {
            _cancellationTokenManager.Cancel();
        }
    }
}
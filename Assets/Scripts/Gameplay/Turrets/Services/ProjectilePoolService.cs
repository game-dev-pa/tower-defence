#nullable enable
using Gameplay.Turrets.Views;
using Shared.Pooling;
using UnityEngine;

namespace Gameplay.Turrets.Services
{
    public sealed class ProjectilePoolService : IProjectilePoolService
    {
        private readonly GameObject _projectilePrefab;
        private readonly Transform _parentHolder;
        private readonly ObjectPool<ProjectileControllerView> _objectPool;

        public ProjectilePoolService(GameObject projectilePrefab, Transform parentHolder)
        {
            _projectilePrefab = projectilePrefab;
            _parentHolder = parentHolder;

            _objectPool = new ObjectPool<ProjectileControllerView>(
                Create,
                onGetFromPoolCallback: GetFromPool,
                onReturnToPoolCallback: ReturnToPool
            );
        }

        public void Spawn(
            Vector3 position,
            Transform target,
            int damage,
            float speed,
            float freezeAmount,
            float freezeDuration)
        {
            var projectile = _objectPool.GetFromPool();
            projectile.transform.position = position;

            projectile.Launch(
                target,
                damage,
                speed,
                freezeAmount,
                freezeDuration,
                OnHitCallback
            );
        }

        private ProjectileControllerView Create()
        {
            var projectileObject = Object.Instantiate(_projectilePrefab, _parentHolder);
            projectileObject.SetActive(false);
            var projectileController = projectileObject.GetComponent<ProjectileControllerView>();
            return projectileController;
        }

        private void GetFromPool(ProjectileControllerView projectileControllerView)
        {
            projectileControllerView.OnGetFromPool();
        }

        private void ReturnToPool(ProjectileControllerView projectileControllerView)
        {
            projectileControllerView.OnReturnToPool();
        }

        private void OnHitCallback(ProjectileControllerView projectileControllerView)
        {
            _objectPool.ReturnToPool(projectileControllerView);
        }
    }
}
using Config;
using JetBrains.Annotations;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Gameplay.Turrets.Services
{
    [UsedImplicitly]
    public sealed class TurretPlacerService : ITurretPlacerService
    {
        private readonly Transform _parentHolder;
        private readonly IObjectResolver _objectResolver;

        public TurretPlacerService(Transform parentHolder, IObjectResolver objectResolver)
        {
            _parentHolder = parentHolder;
            _objectResolver = objectResolver;
        }

        public bool TryPlaceTurret(TurretData turretData, Vector3 position)
        {
            if (turretData.Prefab == null)
            {
                Debug.LogWarning("Turret prefab is missing!");
                return false;
            }

            var turretObject = Object.Instantiate(turretData.Prefab, position, Quaternion.identity, _parentHolder);
            _objectResolver.InjectGameObject(turretObject);
            return true;
        }
    }
}
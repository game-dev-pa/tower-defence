using System;
using JetBrains.Annotations;
using UnityEngine.InputSystem;

namespace Input
{
    [UsedImplicitly]
    public sealed class GameplayInputService : IGameplayInputService, PlayerActions.IGameplayActions
    {
        private readonly PlayerActions _playerActions;

        public event Action OnTurretPlaced; // TOD: Use generic Name rather then turret here.

        public GameplayInputService(PlayerActions playerActions)
        {
            _playerActions = playerActions;

            _playerActions.Gameplay.SetCallbacks(this);
            _playerActions.Gameplay.Enable();
        }

        void IDisposable.Dispose()
        {
            _playerActions.Gameplay.Disable();
        }

        public void OnPlaceTurret(InputAction.CallbackContext context)
        {
            if (!context.started)
                return;

            OnTurretPlaced?.Invoke();
        }
    }
}
using Application.Services;
using Camera;
using Config;
using Gameplay.Turrets.Services;
using Input;
using UI.Views;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using VContainer;

namespace Gameplay.Turrets.Views
{
    public sealed class TurretPlacerManagerView : MonoBehaviour
    {
        [SerializeField] private LayerMask _groundMask;

        private TurretData _selectedTurretData;
        private ICameraProviderService _cameraProviderService;
        private IGameplayInputService _gameplayInputService;
        private ITurretPlacerService _turretPlacerService;
        private IGameManagerService _gameManagerService;
        private UIManagerView _uiManagerView;

        [Inject]
        public void Construct(
            ITurretPlacerService turretPlacerService,
            IGameManagerService gameManagerService,
            IGameplayInputService gameplayInputService,
            ICameraProviderService cameraProviderService,
            UIManagerView uiManagerView)
        {
            _turretPlacerService = turretPlacerService;
            _gameManagerService = gameManagerService;
            _gameplayInputService = gameplayInputService;
            _cameraProviderService = cameraProviderService;
            _uiManagerView = uiManagerView;

            _gameplayInputService.OnTurretPlaced += HandleTurretPlacement;
        }

        public void SetSelectedTurret(TurretData turretData)
        {
            _selectedTurretData = turretData;
        }

        private void OnDestroy()
        {
            if (_gameplayInputService != null)
                _gameplayInputService.OnTurretPlaced -= HandleTurretPlacement;
        }

        private void HandleTurretPlacement()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;

            if (_selectedTurretData == null || _gameManagerService.CurrentCoins < _selectedTurretData.Cost)
            {
                _uiManagerView.FlashInsufficientFunds();
                return;
            }

            var ray = _cameraProviderService.MainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());

            if (!Physics.Raycast(ray, out var hit, 100f, _groundMask))
                return;

            if (_turretPlacerService.TryPlaceTurret(_selectedTurretData, hit.point))
            {
                _gameManagerService.RemoveCoins(_selectedTurretData.Cost);
            }
        }
    }
}
using JetBrains.Annotations;

namespace Camera
{
    [UsedImplicitly]
    public sealed class CameraProviderService : ICameraProviderService
    {
        private UnityEngine.Camera _mainCamera;

        public UnityEngine.Camera MainCamera => _mainCamera ? _mainCamera : _mainCamera = UnityEngine.Camera.main;
    }
}
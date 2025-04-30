using UnityEngine;
using VContainer;

namespace Shared.PauseGame
{
    public abstract class PausableViewBase : MonoBehaviour, IPausable
    {
        private IPausableRegister _pausableRegister;

        public bool IsPaused { get; private set; }

        [Inject]
        public void Construct(IPausableRegister pausableRegister)
        {
            _pausableRegister = pausableRegister;

            _pausableRegister.Register(this);
        }

        public void Pause()
        {
            IsPaused = true;
        }

        public void Resume()
        {
            IsPaused = false;
        }

        protected void OnEnable()
        {
            Resume();
        }

        protected virtual void Update()
        {
            if (IsPaused)
                return;

            PausableUpdate();
        }

        protected void OnDisable()
        {
            Pause();
        }

        protected virtual void OnDestroy()
        {
            _pausableRegister?.Unregister(this);
        }

        protected abstract void PausableUpdate();
    }
}
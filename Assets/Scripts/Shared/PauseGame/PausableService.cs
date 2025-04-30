using System.Collections.Generic;
using JetBrains.Annotations;

namespace Shared.PauseGame
{
    [UsedImplicitly]
    public sealed class PausableService : IPausableService, IPausableRegister
    {
        private readonly HashSet<IPausable> _pausables = new HashSet<IPausable>();

        public void Register(IPausable pausable)
        {
            _pausables.Add(pausable);
        }

        public void Unregister(IPausable pausable)
        {
            _pausables.Remove(pausable);
        }

        public void PauseAll()
        {
            foreach (var pausable in _pausables)
                pausable.Pause();
        }

        public void ResumeAll()
        {
            foreach (var pausable in _pausables)
                pausable.Resume();
        }
    }
}
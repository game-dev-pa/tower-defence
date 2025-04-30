#nullable enable
using System.Threading;
using JetBrains.Annotations;

namespace Shared.Utilities
{
    [UsedImplicitly]
    public sealed class CancellationTokenManager : ICancellationTokenManager
    {
        private CancellationTokenSource? _cancellationTokenSource;
        public CancellationToken Token => _cancellationTokenSource?.Token ?? CancellationToken.None;

        public void StartNewCancellationToken()
        {
            _cancellationTokenSource?.Cancel();
            _cancellationTokenSource = new CancellationTokenSource();
        }

        public void Cancel()
        {
            _cancellationTokenSource?.Cancel();
        }
    }
}
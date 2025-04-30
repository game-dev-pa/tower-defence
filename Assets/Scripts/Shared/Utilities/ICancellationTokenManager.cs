using System.Threading;

namespace Shared.Utilities
{
    public interface ICancellationTokenManager
    {
        CancellationToken Token { get; }
        void StartNewCancellationToken();
        void Cancel();
    }
}
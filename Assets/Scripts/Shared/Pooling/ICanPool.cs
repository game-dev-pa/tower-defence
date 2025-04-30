namespace Shared.Pooling
{
    public interface ICanPool
    {
        void OnGetFromPool();
        void OnReturnToPool();
    }
}
namespace Shared.PauseGame
{
    public interface IPausableRegister
    {
        void Register(IPausable pausable);
        void Unregister(IPausable pausable);
    }
}
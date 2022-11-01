namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPoolBuilder<T> where T : IPoolable
    {
        IPoolBuilder<T> WithObjectProvider(IPoolableObjectProvider<T> poolableObjectProvider);
        IPool<T> GetPool();
    }
}
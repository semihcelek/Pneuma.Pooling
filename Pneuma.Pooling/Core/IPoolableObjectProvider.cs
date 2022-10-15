namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPoolableObjectProvider<T> where T : IPoolable
    {
        T Create();
    }
}
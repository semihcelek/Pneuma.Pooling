namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPool<T> where T : IPoolable 
    {
        T GetFromPool();

        int GetObjectCount();
        int GetActiveObjectCount();
    }
}
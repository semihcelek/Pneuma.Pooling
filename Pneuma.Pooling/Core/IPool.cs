namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPool<T> where T : IPoolable 
    {
        T GetObjectFromPool();

        int GetInActiveObjectCount();
        int GetActiveObjectCount();
    }
}
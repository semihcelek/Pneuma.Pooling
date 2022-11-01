namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPool<T>
    {
        T GetFromPool();

        int GetObjectCount();
        int GetActiveObjectCount();
    }
}
namespace Pneuma.Pooling.Core
{
    public interface IPool<out T>
    {
        T GetFromPool();

        int GetObjectCount();
        int GetActiveObjectCount();
    }
}
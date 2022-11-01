namespace SemihCelek.Pneuma.Pooling.Core
{
    public readonly struct PoolSettings
    {
        public readonly int MaxPoolSize;
            
        public PoolSettings(int maxPoolSize)
        {
            MaxPoolSize = maxPoolSize;
        }
    }
}
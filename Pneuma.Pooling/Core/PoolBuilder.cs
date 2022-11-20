using System;

namespace Pneuma.Pooling.Core
{
    public sealed class PoolBuilder<T> where T : IPoolable
    {
        private IPoolableObjectProvider<T> _poolableObjectProvider;

        private PoolType _poolType;

        public PoolBuilder<T> StartPoolBuildingProcess(PoolType poolType)
        {
            _poolType = poolType;

            return this;
        }

        private IPool<T> CreatePoolInternal<T>() where T : IPoolable
        {
            return _poolType switch
            {
                PoolType.StackPool => new StackPool<T>(),
                _ => throw new Exception("The pool type doesn't exist, please be sure of the type of the pool")
            };
        }

        public PoolBuilder<T> WithObjectProvider(IPoolableObjectProvider<T> poolableObjectProvider)
        {
            _poolableObjectProvider = poolableObjectProvider;
            
            return this;
        }

        public IPool<T> GetPool()
        {
             IPool<T> createdPool = CreatePoolInternal<T>();
            
            return createdPool;
        }
    }

    public enum PoolType
    {
        StackPool,
    }
}
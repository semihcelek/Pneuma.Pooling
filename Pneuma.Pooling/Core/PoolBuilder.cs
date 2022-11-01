using System;
using SemihCelek.Pneuma.Pooling.Examples;

namespace SemihCelek.Pneuma.Pooling.Core
{
    public sealed class PoolBuilder<T> : IPoolBuilder<T> where T : IPoolable
    {
        private IPoolableObjectProvider<T> _poolableObjectProvider;

        private PoolType _poolType;

        public IPoolBuilder<T> StartPoolBuildingProcess<T>(PoolType poolType) where T : IPoolable
        {
            _poolType = poolType;
            
            return this as IPoolBuilder<T>;
        }

        private IPool<T> CreatePoolInternal<T>() where T : IPoolable
        {
            return _poolType switch
            {
                PoolType.StackPool => new StackPool<T>(),
                _ => throw new Exception("The pool type doesn't exist, please be sure of the type of pool")
            };
        }

        public IPoolBuilder<T> WithObjectProvider(IPoolableObjectProvider<T> poolableObjectProvider)
        {
            throw new NotImplementedException();
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
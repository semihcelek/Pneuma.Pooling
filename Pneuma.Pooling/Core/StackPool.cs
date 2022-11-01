using System;
using System.Collections.Generic;

namespace Pneuma.Pooling.Core
{
    public sealed class StackPool<T> : IPool<T> where T : IPoolable
    {
        private readonly Stack<T> _pool;

        private readonly IPoolableObjectProvider<T> _instanceProvider;

        private int _activeObjectCount;

        public StackPool(IPoolableObjectProvider<T> instanceProvider = null)
        {
            if (instanceProvider != null)
            {
                _instanceProvider = instanceProvider;
            }
            
            PoolSettings defaultPoolSettings = new PoolSettings(128);
            
            _pool = new Stack<T>(defaultPoolSettings.MaxPoolSize);
        }

        public StackPool(PoolSettings poolSettings, IPoolableObjectProvider<T> instanceProvider = null)
        {
            if (instanceProvider != null)
            {
                _instanceProvider = instanceProvider;
            }
            
            _pool = new Stack<T>(poolSettings.MaxPoolSize);
        }

        public T GetFromPool()
        {
            return GetObjectFromPoolInternal();
        }

        private T GetObjectFromPoolInternal()
        {
            if (_pool.Count <= 0)
            {
                T createdObject = _instanceProvider != null
                    ? _instanceProvider.Create()
                    : Activator.CreateInstance<T>();

                _activeObjectCount++;
                createdObject.Activate();
                createdObject.ReturnPoolEvent += OnReturnPoolEvent;
                
                return createdObject;
            }

            T objectFromPool = _pool.Pop();
            objectFromPool.Reset();
            
            return objectFromPool;
        }

        private void OnReturnPoolEvent(IPoolable objectToClaim)
        {
            objectToClaim.Reset();
            objectToClaim.ReturnPoolEvent -= OnReturnPoolEvent;

            _pool.Push((T) objectToClaim);
            _activeObjectCount--;
        }

        public int GetObjectCount()
        {
            return _pool.Count + _activeObjectCount;
        }

        public int GetActiveObjectCount()
        {
            return _activeObjectCount;
        }
    }
}
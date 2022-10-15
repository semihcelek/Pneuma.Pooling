using System;
using System.Collections.Generic;

namespace SemihCelek.Pneuma.Pooling.Core
{
    public class StackPool<T> : IPool<T> where T : IPoolable
    {
        private readonly Stack<T> _pool;

        private int _activeObjectCount;

        public StackPool()
        {
            PoolSettings defaultPoolSettings = new PoolSettings(128);
            
            _pool = new Stack<T>(defaultPoolSettings.MaxPoolSize);
        }

        public StackPool(PoolSettings poolSettings)
        {
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
                T createdObject = Activator.CreateInstance<T>();

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

        public readonly struct PoolSettings
        {
            public readonly int MaxPoolSize;
            
            public PoolSettings(int maxPoolSize)
            {
                MaxPoolSize = maxPoolSize;
            }
        }
    }
}
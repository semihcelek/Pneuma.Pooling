using System;
using System.Collections.Generic;

namespace SemihCelek.Pneuma.Pooling.Core
{
    public class StackPool<T> : IPool<T> where T : IPoolable
    {
        private readonly Queue<T> _pool;

        private int _activeObjectCount;

        public StackPool()
        {
            PoolSettings defaultPoolSettings = new PoolSettings(128);
            
            _pool = new Queue<T>(defaultPoolSettings.MaxPoolSize);
        }

        public StackPool(PoolSettings poolSettings)
        {
            _pool = new Queue<T>(poolSettings.MaxPoolSize);
        }

        public T GetObjectFromPool()
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

            T objectFromPool = _pool.Dequeue();
            objectFromPool.Reset();
            
            return objectFromPool;
        }

        private void OnReturnPoolEvent(IPoolable objectToClaim)
        {
            objectToClaim.Reset();
            objectToClaim.ReturnPoolEvent -= OnReturnPoolEvent;

            _pool.Enqueue((T) objectToClaim);
            _activeObjectCount--;
        }

        public int GetInActiveObjectCount()
        {
            return _pool.Count;
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
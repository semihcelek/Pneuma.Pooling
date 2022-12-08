using System;
using System.Collections.Generic;
using Pneuma.Pooling.Utilities;

namespace Pneuma.Pooling.Core
{
    public sealed class PoolContainer
    {
        private Dictionary<int, IPool<IPoolable>> _poolDictionary = new();

        private int _poolRegisterIndex;

        public int RegisterPool(IPool<IPoolable> pool, bool allowMultiple = false)
        {
            if (_poolDictionary.ContainsValue(pool) && !allowMultiple)
            {
                int key = _poolDictionary.GetKeyFromValue(pool);
                return key;
            }

            ++_poolRegisterIndex;
            _poolDictionary.Add(_poolRegisterIndex, pool);
            return _poolRegisterIndex;
        }

        public IPool<T> GetPoolFromHash<T>(int registeredPoolHash) where T : IPoolable
        {
            if (_poolDictionary.ContainsKey(registeredPoolHash))
            {
                return (IPool<T>)_poolDictionary[registeredPoolHash];
            }

            throw new Exception("Requested pool has not been registered");
        }

        public void DisposePools()
        {
            _poolDictionary.Clear();
        }
    }
}
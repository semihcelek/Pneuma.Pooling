using System;
using System.Collections.Generic;
using Pneuma.Pooling.Utilities;

namespace Pneuma.Pooling.Core
{
    public static class PoolContainer
    {
        private static Dictionary<int, IPool<IPoolable>> _poolDictionary = new();

        private static int _poolRegisterIndex;
        
        public static int RegisterPool(IPool<IPoolable> pool, bool allowMultiple = false)
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

        public static IPool<T> GetPoolFromHash<T>(int registeredPoolHash) where T : IPoolable
        {
            if (_poolDictionary.ContainsKey(registeredPoolHash))
            {
                return (IPool<T>)_poolDictionary[registeredPoolHash];
            }

            throw new Exception("Requested pool has not been registered");
        }

        public static void DisposePools()
        {
            _poolDictionary.Clear();
        }
    }
}
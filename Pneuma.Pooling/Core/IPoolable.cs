using System;

namespace Pneuma.Pooling.Core
{
    public interface IPoolable
    {
        event Action<IPoolable> ReturnPoolEvent;
        
        void ReturnPool();
        void Activate();
        void Reset();
    }
}
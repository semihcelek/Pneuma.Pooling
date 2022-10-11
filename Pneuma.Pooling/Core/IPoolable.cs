using System;

namespace SemihCelek.Pneuma.Pooling.Core
{
    public interface IPoolable
    {
        event Action<IPoolable> ReturnPoolEvent;
        
        void ReturnPool(IPoolable poolObject);
        void Activate();
        void Reset();
    }
}
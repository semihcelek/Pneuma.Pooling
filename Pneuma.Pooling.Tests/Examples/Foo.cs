using System;
using Pneuma.Pooling.Core;

namespace Pneuma.Pooling.Test.Examples
{
    public  class Foo : IPoolable
    {
        public event Action<IPoolable> ReturnPoolEvent;
        
        public Status Status { get; private set; } = Status.Default;

        public int Id;

        public void ReturnPool()
        {
            ReflectState(Status.ReturnToPool);
            ReturnPoolEvent?.Invoke(this);
        }

        public void Activate()
        {
            ReflectState(Status.Active);
        }

        public void Reset()
        {
            ReflectState(Status.Reset);
        }

        private void ReflectState(Status status)
        {
            Status = status;
            Console.WriteLine(status.ToString());
        }
    }

    public enum Status
    {
        Active,
        Reset,
        ReturnToPool,
        Default,
    }
}
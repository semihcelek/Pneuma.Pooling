using System;
using SemihCelek.Pneuma.Pooling.Core;

namespace SemihCelek.Pneuma.Pooling
{
    public class MyType : IPoolable
    {
        public event Action<IPoolable> ReturnPoolEvent;
        
        public Status Status { get; private set; } = Status.Default;

        public int Id;

        public void ReturnPool(IPoolable poolObject)
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

        public override bool Equals(object obj)
        {
            MyType candidateObject = obj as MyType;

            if (candidateObject is null)
            {
                return false;
            }

            return CompareProperties(candidateObject);
        }

        private bool CompareProperties(MyType candidateObject)
        {
            return Id == candidateObject.Id;
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
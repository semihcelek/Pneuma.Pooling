using SemihCelek.Pneuma.Pooling.Core;

namespace SemihCelek.Pneuma.Pooling.Examples
{
    public class FooInstanceProvider : IPoolableObjectProvider<Foo>
    {
        public Foo Create()
        {
            return new Foo();
        }
    }
}
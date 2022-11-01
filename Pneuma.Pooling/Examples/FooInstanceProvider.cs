using Pneuma.Pooling.Core;

namespace Pneuma.Pooling.Examples
{
    public class FooInstanceProvider : IPoolableObjectProvider<Foo>
    {
        public Foo Create()
        {
            return new Foo();
        }
    }
}
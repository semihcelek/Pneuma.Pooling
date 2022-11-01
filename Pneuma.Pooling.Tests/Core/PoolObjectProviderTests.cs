using Pneuma.Pooling.Core;
using Pneuma.Pooling.Examples;
using Xunit;

namespace Pneuma.Pooling.Test.Core
{
    public class PoolObjectProviderTests
    {
        [Fact]
        public void Poolable_Object_Provider_Returns_Poolable_Object()
        {
            IPoolableObjectProvider<Foo> poolableObjectProvider = new FooInstanceProvider();

            object poolable = poolableObjectProvider.Create();
            
            Assert.IsAssignableFrom<IPoolable>(poolable);
        }

        [Fact]
        public void Create_Pool_Utilizes_Poolable_Object_Provider()
        {
            IPoolableObjectProvider<Foo> poolableObjectProvider = new FooInstanceProvider();

            IPool<Foo> fooPool = new StackPool<Foo>(poolableObjectProvider);

            Assert.IsAssignableFrom<IPoolable>(fooPool.GetFromPool());
        } 
    }
}
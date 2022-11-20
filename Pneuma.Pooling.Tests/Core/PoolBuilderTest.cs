using Pneuma.Pooling.Core;
using Pneuma.Pooling.Test.Examples;
using Xunit;

namespace Pneuma.Pooling.Test.Core
{
    public class PoolBuilderTest
    {
        [Fact]
        public void Create_Pool_With_PoolBuilder()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).GetPool();

            Foo poolable = fooPool.GetFromPool();

            Assert.NotNull(poolable);
            Assert.IsAssignableFrom<IPoolable>(poolable);
        }
        
        [Fact]
        public void Create_Pool_With_PoolBuilder_Using_Object_Provider()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPoolableObjectProvider<Foo> fooProvider = new FooInstanceProvider();
            
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).WithObjectProvider(fooProvider).GetPool();
            
            Foo poolable = fooPool.GetFromPool();
            
            Assert.NotNull(poolable);
            Assert.IsAssignableFrom<IPoolable>(poolable);
        }
    }
}
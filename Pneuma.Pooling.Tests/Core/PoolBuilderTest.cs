using SemihCelek.Pneuma.Pooling.Core;
using SemihCelek.Pneuma.Pooling.Examples;
using Xunit;

namespace SemihCelek.Pneuma.Pooling.Test.Core
{
    public class PoolBuilderTest
    {
        [Fact]
        public void Create_Pool_With_PoolBuilder()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess<Foo>(PoolType.StackPool).GetPool();

            Foo poolable = fooPool.GetFromPool();

            Assert.NotNull(poolable);
            Assert.IsAssignableFrom<IPoolable>(poolable);
        }
        
        [Fact]
        public void Create_Pool_Using_Object_Provider_With_PoolBuilder()
        {
            // PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            // IPoolableObjectProvider<Foo> fooProvider = new FooInstanceProvider();
            //
            // IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess<Foo>(PoolType.StackPool).WithObjectProvider(fooBuilder).GetPool();
            //
            // Foo poolable = fooPool.GetFromPool();
            //
            // Assert.NotNull(poolable);
            // Assert.IsAssignableFrom<IPoolable>(poolable);
        }
    }
}
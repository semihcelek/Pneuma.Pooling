using System;
using Pneuma.Pooling.Core;
using Pneuma.Pooling.Test.Examples;
using Xunit;

namespace Pneuma.Pooling.Test.Core
{
    public class PoolContainerTest
    {
        private int _firstRegisteredPoolHash;
        private int _secondRegisteredPoolHash;

        public PoolContainerTest()
        {
            Register_Pool_To_PoolContainer();
            Register_Pool_Utilizing_Provider();
        }
        
        private void Register_Pool_To_PoolContainer()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).GetPool();

            _firstRegisteredPoolHash = PoolContainer.RegisterPool(fooPool);
        }

        private void Register_Pool_Utilizing_Provider()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPoolableObjectProvider<Foo> fooProvider = new FooInstanceProvider();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).WithObjectProvider(fooProvider).GetPool();

            _secondRegisteredPoolHash = PoolContainer.RegisterPool(fooPool);
        }

        [Fact]
        private void Use_Pool_From_Pool_Collection()
        {
            IPool<Foo> fooPool = PoolContainer.GetPoolFromHash<Foo>(_firstRegisteredPoolHash);
            
            Assert.NotNull(fooPool);

            Foo foo = fooPool.GetFromPool();
            
            Assert.IsType<Foo>(foo);
        }

        [Fact]
        private void Use_Pool_From_Pool_Collection_With_ObjectProvider()
        {
            IPool<Foo> fooPool = PoolContainer.GetPoolFromHash<Foo>(_secondRegisteredPoolHash);
            
            Assert.NotNull(fooPool);

            Foo foo = fooPool.GetFromPool();
            
            Assert.IsType<Foo>(foo);
        }
    }
}
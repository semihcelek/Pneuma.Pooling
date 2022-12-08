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

        private PoolContainer _poolContainer;

        public PoolContainerTest()
        {
            _poolContainer = new PoolContainer();
            
            Register_Pool_To_PoolContainer();
            Register_Pool_Utilizing_Provider();
        }
        
        private void Register_Pool_To_PoolContainer()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).GetPool();

            _firstRegisteredPoolHash = _poolContainer.RegisterPool(fooPool);
        }

        private void Register_Pool_Utilizing_Provider()
        {
            PoolBuilder<Foo> fooBuilder = new PoolBuilder<Foo>();
            IPoolableObjectProvider<Foo> fooProvider = new FooInstanceProvider();
            IPool<Foo> fooPool = fooBuilder.StartPoolBuildingProcess(PoolType.StackPool).WithObjectProvider(fooProvider).GetPool();

            _secondRegisteredPoolHash = _poolContainer.RegisterPool(fooPool);
        }

        [Fact]
        private void Use_Pool_From_Pool_Collection()
        {
            IPool<Foo> fooPool = _poolContainer.GetPoolFromHash<Foo>(_firstRegisteredPoolHash);
            
            Assert.NotNull(fooPool);

            Foo foo = fooPool.GetFromPool();
            
            Assert.IsType<Foo>(foo);
        }

        [Fact]
        private void Use_Pool_From_Pool_Collection_With_ObjectProvider()
        {
            IPool<Foo> fooPool = _poolContainer.GetPoolFromHash<Foo>(_secondRegisteredPoolHash);
            
            Assert.NotNull(fooPool);

            Foo foo = fooPool.GetFromPool();
            
            Assert.IsType<Foo>(foo);
        }
    }
}
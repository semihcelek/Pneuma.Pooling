using SemihCelek.Pneuma.Pooling.Core;
using SemihCelek.Pneuma.Pooling.Examples;
using Xunit;

namespace SemihCelek.Pneuma.Pooling.Test.Core
{
    public class PoolTests
    {
        private readonly IPool<Foo> _poolablePool;

        public PoolTests()
        {
            _poolablePool = new StackPool<Foo>();
        }

        [Fact]
        public void Create_Pool()
        {
            Assert.NotNull(_poolablePool);
        }

        [Fact]
        public void Get_And_Return_Item_To_Pool()
        {
            IPoolable poolable = _poolablePool.GetFromPool();

            poolable.ReturnPool();

            Assert.Equal(1, _poolablePool.GetObjectCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(60)]
        [InlineData(128)]
        public void Get_Number_Of_Items_And_Returned_To_Pool(int number)
        {
            IPoolable[] poolables = new Foo[number];

            int initialActivePoolObjectCount = _poolablePool.GetActiveObjectCount();
            int initialPoolObjectCount = _poolablePool.GetObjectCount();

            for (int index = 0; index < number; index++)
            {
                poolables[index] = _poolablePool.GetFromPool();
            }

            for (var index = 0; index < poolables.Length; index++)
            {
                IPoolable poolable = poolables[index];

                poolable.ReturnPool();
            }

            Assert.Equal(number + initialPoolObjectCount, _poolablePool.GetObjectCount());
            Assert.Equal(initialActivePoolObjectCount, _poolablePool.GetActiveObjectCount());
        }

        [Fact]
        public void Check_Whether_Pool_Returns_Same_Object()
        {
            IPoolable poolable = _poolablePool.GetFromPool();

            poolable.ReturnPool();

            IPoolable otherPoolable = _poolablePool.GetFromPool();

            Assert.Equal(poolable, otherPoolable);
        }
    }
}
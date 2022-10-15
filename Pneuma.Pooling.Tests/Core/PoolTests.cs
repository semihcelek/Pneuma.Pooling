using SemihCelek.Pneuma.Pooling.Core;
using Xunit;

namespace SemihCelek.Pneuma.Pooling.Test.Core
{
    public class PoolTests
    {
        private readonly IPool<Foo> _fooPool;

        public PoolTests()
        {
            _fooPool = new StackPool<Foo>();
        }

        [Fact]
        public void Create_Pool()
        {
            Assert.NotNull(_fooPool);
        }

        [Fact]
        public void Get_Item_With_Active_State_From_New_Pool()
        {
            Foo foo = _fooPool.GetFromPool();

            Assert.Equal(Status.Active, foo.Status);
            
            foo.ReturnPool();
        }

        [Fact]
        public void Return_Item_To_Pool_From_New_Pool()
        {
            Foo foo = _fooPool.GetFromPool();
            
            foo.ReturnPool();

            Assert.Equal(1, _fooPool.GetObjectCount());
        }

        [Fact]
        public void Check_Returned_Item_State_From_New_Pool()
        {
            Foo foo = _fooPool.GetFromPool();
            
            foo.ReturnPool();
            
            Assert.Equal(Status.Reset, foo.Status);
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(60)]
        [InlineData(128)]
        public void Get_Number_Of_Items_Returned_To_Pool_From_New_Pool(int number)
        {
            Foo[] foos = new Foo[number];
            
            for (int index = 0; index < number; index++)
            {
                foos[index] = _fooPool.GetFromPool();
            }

            for (var index = 0; index < foos.Length; index++)
            {
                Foo foo = foos[index];
                
                foo.ReturnPool();
            }

            Assert.Equal(number, _fooPool.GetObjectCount());
            Assert.Equal(0, _fooPool.GetActiveObjectCount());
        }

        [Fact]
        public void Check_Whether_Returns_Same_Object_From_New_Pool()
        {
            Foo foo = _fooPool.GetFromPool();

            foo.ReturnPool();

            Foo otherFoo = _fooPool.GetFromPool();
            
            Assert.Equal(foo , otherFoo);            
        }
    }
}
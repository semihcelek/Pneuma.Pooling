using SemihCelek.Pneuma.Pooling.Core;
using Xunit;

namespace SemihCelek.Pneuma.Pooling.Test.Core
{
    public class PoolTests
    {
        [Fact]
        public void Create_Pool_Without_Instance_Provider()
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();
            
            Assert.NotNull(myTypePool);
        }

        [Fact]
        public void Get_Item_From_Pool_Without_Instance_Provider()
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();
            
            MyType dummyType = new MyType();
            
            Assert.True(dummyType.Equals(myTypePool.GetObjectFromPool()));
        }

        [Fact]
        public void Get_Item_With_Active_State_Without_Instance_Provider()
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();

            MyType myType = myTypePool.GetObjectFromPool();

            Assert.Equal(Status.Active, myType.Status);
        }

        [Fact]
        public void Call_Return_Item_To_Pool_Method()
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();

            MyType myType = myTypePool.GetObjectFromPool();
            
            myType.ReturnPool(myType);
            
            Assert.Equal(Status.Reset, myType.Status);
        }
        
        [Fact]
        public void Return_Item_To_Pool()
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();

            MyType myType = myTypePool.GetObjectFromPool();
            
            myType.ReturnPool(myType);

            Assert.Equal(1, myTypePool.GetInActiveObjectCount());
        }

        [Theory]
        [InlineData(3)]
        [InlineData(60)]
        [InlineData(128)]
        public void Get_Number_Of_Items_From_Pool(int number)
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();

            for (int index = 0; index < number; index++)
            {
                myTypePool.GetObjectFromPool();
            }
            
            Assert.Equal(number, myTypePool.GetActiveObjectCount());
        }
        
        [Theory]
        [InlineData(3)]
        [InlineData(60)]
        [InlineData(128)]
        public void Get_Number_Of_Items_Returned_To_Pool(int number)
        {
            IPool<MyType> myTypePool = new StackPool<MyType>();

            MyType[] myTypes = new MyType[number];
            
            for (int index = 0; index < number; index++)
            {
                myTypes[index] = myTypePool.GetObjectFromPool();
            }

            foreach (MyType myType in myTypes)
            {
                myType.ReturnPool(myType);
            }
            
            Assert.Equal(number, myTypePool.GetInActiveObjectCount());
            Assert.Equal(0, myTypePool.GetActiveObjectCount());
        }
    }
}
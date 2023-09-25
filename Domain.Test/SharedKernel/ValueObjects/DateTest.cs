using Common.Utilities;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class DateTest
    {
        [Fact]
        public void LessThan_Compare_ReturnTrue()
        {

            var date1 = MockHelper.PrivateMock<Date>(DateTime.Now);

            var date2 = MockHelper.PrivateMock<Date>(DateTime.Now.Date.AddDays(2));

            var date3 = MockHelper.PrivateMock<Date>(DateTime.Now);


            Assert.True(date1 < date2);
            Assert.True(date1 <= date2);
            Assert.True(date1 <= date3);
            Assert.False(date1 < date3);
        }


        [Fact]
        public void GreaterThan_Compare_ReturnTrue()
        {
            var date1 = MockHelper.PrivateMock<Date>(DateTime.Now.Date.AddDays(2));

            var date2 = MockHelper.PrivateMock<Date>(DateTime.Now);

            var date3 = MockHelper.PrivateMock<Date>(DateTime.Now);

            Assert.True(date1 > date2);
            Assert.True(date1 >= date2);
            Assert.True(date2 >= date3);
            Assert.False(date2 > date3);
        }

       

    }
}

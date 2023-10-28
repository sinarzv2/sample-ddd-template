using Common.Resources;
using Common.Resources.Messages;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class FirstNameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string value)
        {
            var result = FirstName.Create(value);
            var errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }




        [Theory]
        [InlineData("Ali")]
        [InlineData("  Ali  ")]
        public void Create_SendSomeName_ResultIsSuccess(string value)
        { 
            var result = FirstName.Create(value);

            Assert.True(result.IsSuccess);
            Assert.Equal("Ali", actual: result.Data.Value);
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }


        [Theory]
        [InlineData("  Ali  Reza  ")]
        [InlineData("  Ali     Reza  ")]
        public void Create_SendAnotherName_ResultIsSuccess(string value)
        {
            var result = FirstName.Create(value);

            Assert.True(result.IsSuccess);
            Assert.Equal("Ali Reza", actual: result.Data.Value);
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }

       
    }
}

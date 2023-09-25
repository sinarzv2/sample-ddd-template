using Common.Resources.Messages;
using Common.Resources;
using Domain.SharedKernel.ValueObjects;
using Common.Utilities;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class NameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string value)
        {
            var result = Name.Create(value);
            var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendMorThanMaxLength_ResultIsNotSuccess()
        {
            var value = string.Empty;

            for (var index = 1; index <= Name.MaxLength + 1; index++)
            {
                value += "a";
            }

            var result = Name.Create(value);
            var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Name, Name.MaxLength);


            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Theory]
        [InlineData("Ali")]
        [InlineData("  Ali  ")]
        public void Create_SendSomeName_ResultIsSuccess(string value)
        {
            var result = Name.Create(value);

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
            var result = Name.Create(value);

            Assert.True(result.IsSuccess);
            Assert.Equal("Ali Reza", actual: result.Data.Value);
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }
    }
}

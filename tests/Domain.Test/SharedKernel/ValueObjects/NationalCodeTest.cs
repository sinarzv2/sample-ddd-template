using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class NationalCodeTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string value)
        {
            var result = NationalCode.Create(value);

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Required, DataDictionary.NationalCode);

            Assert.Equal(errorMessage, result.Errors[0]);
        }




        [Theory]
        [InlineData("  12345  ")]
        [InlineData("  123451234512345  ")]
        public void Create_SendWrongLength_ResultIsNotSuccess(string value)
        {
            var result = NationalCode.Create(value);

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalCode, NationalCode.FixLength);

            Assert.Equal(errorMessage, result.Errors[0]);
        }


        [Theory]
        [InlineData("1234512345")]
        [InlineData("  1234512345  ")]
        public void Create_SendCorrectNationalCode_ResultIsSuccess(string value)
        {
            var result = NationalCode.Create(value);

            Assert.True(result.IsSuccess);
            Assert.Equal("1234512345", result.Data.Value);
        }


        [Fact]
        public void Create_SendWrongFormat_ResultIsNotSuccess()
        {
            var result = NationalCode.Create(value: "aaaaaaaaaa");

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.NationalCode);

            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
        }
    }
}

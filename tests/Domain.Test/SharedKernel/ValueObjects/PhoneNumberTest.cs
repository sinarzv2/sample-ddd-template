using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class PhoneNumberTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string? value)
        {
            var result = PhoneNumber.Create(value);
            var errorMessage = string.Format(Validations.Required, DataDictionary.PhoneNumber);

            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors[0]);
        }



        [Theory]
        [InlineData("  12345  ")]
        [InlineData("  123451234512345  ")]
        public void Create_SendWorngPhoneNumber_ResultIsNotSuccess(string? value)
        {
            var result = PhoneNumber.Create(value);
            var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.PhoneNumber, PhoneNumber.FixLength);

            Assert.False(result.IsSuccess);
            Assert.Equal(expected: errorMessage, actual: result.Errors[0]);
        }



        [Fact]
        public void Create_SendCorrectPhoneNumber_ResultIsSuccess()
        {
            var result = PhoneNumber.Create("09121087461");

            Assert.True(result.IsSuccess);
            Assert.Equal("09121087461", result.Data.Value);
        }

        [Fact]
        public void Create_SendAnotherCorrectPhoneNumber_ResultIsSuccess()
        {
            var result = PhoneNumber.Create("  09121087461  ");

            Assert.True(result.IsSuccess);
            Assert.False(result.Data.IsVerified);
            Assert.Equal("09121087461", result.Data.Value);
        }

        [Fact]
        public void Create_SendBadFormatPhoneNumber_ResultIsNotSuccess()
        {
            var result = PhoneNumber.Create("09aaaaaaaaa");
            var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.PhoneNumber);

            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
        }

       

        [Fact]
        public void Create_CheckVerifyPhoneNumber_ResultIsSuccess()
        {
            var result = PhoneNumber.Create(value: "  09121087461  ");

            Assert.True(result.IsSuccess);

            Assert.False(result.Data.IsVerified);
            Assert.Equal("09121087461", result.Data.Value);

            var cellPhoneNumber = result.Data;

            var newResult = cellPhoneNumber.Verify();

            Assert.True(condition: newResult.Data.IsVerified);
            Assert.Equal(expected: "09121087461", actual: newResult.Data.Value);

            var successMessage = Successes.PhoneNumberVerified;

            Assert.Equal(successMessage, newResult.Successes[0]);
        }

        [Fact]
        public void Create_CheckVerifyPhoneNumber_ResultIsNotSuccess()
        {
            var result = PhoneNumber.Create(value: "  09121087461  ");

            Assert.True(result.IsSuccess);

            Assert.False(result.Data.IsVerified);
            Assert.Equal("09121087461", result.Data.Value);

            var cellPhoneNumber = result.Data;

            var newResult = cellPhoneNumber.Verify();
            newResult = newResult.Data.Verify();

            var errorMessage = Errors.PhoneNumberAlreadyVerified;

            Assert.Equal(errorMessage, newResult.Errors[0]);
        }
    }
}

using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SharedKernel.Enumerations;

namespace Domain.Test.SharedKernel.Enumerations
{
    public class GenderTest
    {
        [Fact]
        public void GetByValue_SendNull_ResultIsNotSuccess()
        {
            var result = Gender.GetByValue(null);

            var errorMessage = string.Format(Validations.Required, DataDictionary.Gender);

            Assert.False(condition: result.IsSuccess);
            Assert.Equal(errorMessage, result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void GetByValue_SendOutOfRange_ResultIsNotSuccess()
        {
            var result = Gender.GetByValue(10);
            var errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

            Assert.False(condition: result.IsSuccess);
            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void GetByValue_SendMaleValue_ResultIsNotSuccess()
        {
            var result = Gender.GetByValue(0);

            Assert.True(result.IsSuccess);
            Assert.Equal(Gender.Male, result.Data);
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void GetByValue_SendFemaleValue_ResultIsNotSuccess()
        {
            var result = Gender.GetByValue(value: 1);

            Assert.True(condition: result.IsSuccess);
            Assert.Equal(Gender.Female, result.Data);

            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }
    }
}

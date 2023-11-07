using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Identity.ValueObjects;

namespace Domain.Test.Aggregates.Identity.ValueObjects
{
    public class BirthDateTest
    {
        [Fact]
        public void Create_SendNull_ResultIsNotSuccess()
        {
            var result = BirthDate.Create(null);

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Required, DataDictionary.Birthdate);

            Assert.Equal(errorMessage, result.Errors[0]);

            Assert.Single(result.Errors);
        }

        [Fact]
        public void Create_SendLessThanMinValue_ResultIsNotSuccess()
        {
            var result = BirthDate.Create(BirthDate.MinValue.AddDays(-1));

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Range, DataDictionary.Birthdate, BirthDate.MinValue, BirthDate.MaxValue);

            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

            Assert.Single(result.Errors);
        }

        [Fact]
        public void Create_SendMoreThanMinValue_ResultIsNotSuccess()
        {
            var result = BirthDate.Create(BirthDate.MaxValue.AddDays(1));

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Range, DataDictionary.Birthdate, BirthDate.MinValue, BirthDate.MaxValue);

            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

            Assert.Single(result.Errors);
        }

        [Fact]
        public void Create_SendCorrectBirthdate_ResultIsSuccess()
        {
            var myDate = new DateTime(1991, 5, 29);
            var result = BirthDate.Create(myDate);

            Assert.True(result.IsSuccess);
            Assert.Equal(myDate.Date, actual: result.Data.Value);
        }

    }
}

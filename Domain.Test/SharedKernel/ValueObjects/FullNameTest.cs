using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;


namespace Domain.Test.SharedKernel.ValueObjects
{
    public class FullNameTest
    {
        [Fact]
        public void Create_SendAllNull_ResultIsNotSuccess()
        {

            var result = FullName.Create(null, null, null);
            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Required, DataDictionary.Gender);
            Assert.Equal(errorMessage, result.Errors[0]);

            errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);
            Assert.Equal(errorMessage, result.Errors[1]);

            errorMessage = string.Format(Validations.Required, DataDictionary.LastName);
            Assert.Equal(errorMessage, result.Errors[2]);

            Assert.Equal(3, result.Errors.Count);
            Assert.Empty(result.Successes);
        }


        [Fact]
        public void Create_SendFirstNameLastNameNull_ResultIsNotSuccess()
        {

            var result = FullName.Create(Gender.Male.Value, null, null);
            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);
            Assert.Equal(errorMessage, result.Errors[0]);

            errorMessage = string.Format(Validations.Required, DataDictionary.LastName);
            Assert.Equal(errorMessage, result.Errors[1]);

            Assert.Equal(2, result.Errors.Count);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendWrongGender_ResultIsNotSuccess()
        {
            var result = FullName.Create(2, "Ali Reza", "Alavi Asl");
            var errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

            Assert.False(result.IsSuccess);
            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendCorrectValue_ResultIsSuccess()
        {
            var result = FullName.Create(Gender.Male.Value, "    Ali   Reza   ", "      Alavi       Asl  ");

            Assert.True(result.IsSuccess);
            Assert.Equal("Ali Reza", result.Data.FirstName.Value);
            Assert.Equal("Alavi Asl", result.Data.LastName.Value);
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }
    }
}

using Common.Resources;
using Common.Resources.Messages;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects
{
    public class LastNameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string value)
        {
            
            var result = LastName.Create(value);

            // **************************************************
            Assert.False(result.IsSuccess);
            // **************************************************

            // **************************************************
            var errorMessage = string.Format(Validations.Required, DataDictionary.LastName);
            
            Assert.Equal(errorMessage, result.Errors[0]);
            // **************************************************

            // **************************************************
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
            // **************************************************
        }




        [Theory]
        [InlineData("Alavi")]
        [InlineData("  Alavi  ")]
        public void Create_SendSomeName_ResultIsSuccess(string value)
        { 
            var result = LastName.Create(value);

            // **************************************************
            Assert.True(result.IsSuccess);
            // **************************************************

            // **************************************************
            Assert.Equal("Alavi", actual: result.Data.Value);
            // **************************************************

            // **************************************************
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
            // **************************************************
        }


        [Theory]
        [InlineData("  Alavi  Asl  ")]
        [InlineData("  Alavi     Asl  ")]
        public void Create_SendAnotherName_ResultIsSuccess(string value)
        {
            var result = LastName.Create(value);

            // **************************************************
            Assert.True(result.IsSuccess);
            // **************************************************

            // **************************************************
            Assert.Equal("Alavi Asl", actual: result.Data.Value);
            // **************************************************

            // **************************************************
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
            // **************************************************
        }

       
    }
}

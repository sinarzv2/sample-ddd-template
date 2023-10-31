using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Identity.ValueObjects;

namespace Domain.Test.Aggregates.Identity.ValueObjects
{
    public class UsernameTest
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("     ")]
        public void Create_SendNullOrEmpty_ResultIsNotSuccess(string? value)
        {
            var result = Username.Create(value);

            Assert.False(result.IsSuccess);
        
            var errorMessage = string.Format(Validations.Required, DataDictionary.Username);

            Assert.Equal(errorMessage, result.Errors[0]);
      
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }


        [Fact]
        public void Create_SendLessThanMinLength_ResultIsNotSuccess()
        {
            var result = Username.Create("     ALI   ALI     ");
            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.MinLength, DataDictionary.Username, Username.MinLength);

            Assert.Equal(errorMessage, result.Errors[0]);
          
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendMoreThanMaxLength_ResultIsNotSuccess()
        {
            var result = Username.Create
                ("     SEYED SINA FATEMI RAZAVI    SEYED SINA FATEMI RAZAVI   ");

            Assert.False(result.IsSuccess);
           
            var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Username, Username.MaxLength);

            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
          
            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendBadFormat_ResultIsNotSuccess()
        {
            var result = Username.Create("AliReza Alavi");

          
            Assert.False(result.IsSuccess);
          
            var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Username);

            Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

            Assert.Single(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendCorrectUsername_ResultIsSuccess()
        {
            var result = Username.Create("SinaRazavi");

           
            Assert.True(result.IsSuccess);
     
            Assert.Equal("SinaRazavi", result.Data.Value);
           
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }

        [Fact]
        public void Create_SendCorrectUsernameWithSpace_ResultIsSuccess()
        {
            var result = Username.Create("  SinaRazavi  ");

           
            Assert.True(result.IsSuccess);
      
            Assert.Equal("SinaRazavi", result.Data.Value);
          
            Assert.Empty(result.Errors);
            Assert.Empty(result.Successes);
        }
    }
}

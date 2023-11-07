using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Identity;

namespace Domain.Test.Aggregates.Identity
{
    public class RoleTest
    {
        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void Create_SendParameterNull_ResultIsNotSuccess(string? name)
        {
            var result = Role.Create(name);

            Assert.False(result.IsSuccess);

            var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

            Assert.Equal(errorMessage, result.Errors[0]);


            Assert.Empty(result.Successes);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void Create_SendCorrectName_ResultIsSuccess()
        {
            var result = Role.Create("Admin");

            Assert.True(result.IsSuccess);

            Assert.Equal("Admin", result.Data.Name);


            Assert.Empty(result.Successes);
            Assert.Empty(result.Errors);
        }
    }
}

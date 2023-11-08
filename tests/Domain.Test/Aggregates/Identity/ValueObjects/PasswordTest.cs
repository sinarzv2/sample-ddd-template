using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Identity.ValueObjects;

namespace Domain.Test.Aggregates.Identity.ValueObjects;

public class PasswordTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_SendNullOrEmpty_ResultIsNotSuccess(string value)
    {
        var result = Password.Create(value);

     
        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Password);

        Assert.Equal(errorMessage, result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendLessThanMinLength_ResultIsNotSuccess()
    {
        var result = Password.Create("     ALI   ALI     ");
        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.MinLength, DataDictionary.Password, Password.MinLength);

        Assert.Equal(errorMessage, result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendMoreThanMaxLength_ResultIsNotSuccess()
    {
        var result = Password.Create
            ("     SEYED SINA FATEMI RAZAVI    SEYED SINA FATEMI RAZAVI   ");

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Password, Password.MaxLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendBadWithoutNumber_ResultIsNotSuccess()
    {
        var result = Password.Create("Sina Razavi");


        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.ContainNumber, DataDictionary.Password);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendBadFormat_ResultIsNotSuccess()
    {
        var result = Password.Create("Sina Razavi2345");


        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Password);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendCorrectUsername_ResultIsSuccess()
    {
        var result = Password.Create("SinaRazavi1991");


        Assert.True(result.IsSuccess);

        Assert.Equal("SinaRazavi1991", result.Data.Value);

        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendCorrectUsernameWithSpace_ResultIsSuccess()
    {
        var result = Password.Create("  SinaRazavi1991  ");


        Assert.True(result.IsSuccess);

        Assert.Equal("SinaRazavi1991", result.Data.Value);

        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }
}
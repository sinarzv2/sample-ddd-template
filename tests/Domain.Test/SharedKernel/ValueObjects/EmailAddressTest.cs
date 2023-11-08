using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects;

public class EmailAddressTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("         ")]
    public void Create_SendNullOrEmpty_ResultIsNotSuccess(string? value)
    {
        var result = EmailAddress.Create(value);

        var errorMessage = string.Format(Validations.Required, DataDictionary.EmailAddress);
        Assert.False(result.IsSuccess);
        Assert.Equal(errorMessage, result.Errors[0]);
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

        

    [Fact]
    public void Create_SendMorThanMaxLength_ResultIsNotSuccess()
    {
        var value = string.Empty;

        for (var index = 1; index <= EmailAddress.MaxLength + 1; index++)
        {
            value += "a";
        }

        var result = EmailAddress.Create(value);
        var errorMessage = string.Format(Validations.MaxLength, DataDictionary.EmailAddress, EmailAddress.MaxLength);


        Assert.False(result.IsSuccess);
        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendWorngEmailAddress_ResultIsNotSuccess()
    {
        var result = EmailAddress.Create("abcde");
        var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.EmailAddress);
           
        Assert.False(result.IsSuccess);
        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendCorrectEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "30na.rzv2@GMail.com");

        Assert.True(result.IsSuccess);
        Assert.Equal("30na.rzv2@GMail.com", result.Data.Value);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendAnotherCorrectEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "   30na.rzv2@GMail.com   ");

        Assert.True(result.IsSuccess);
        Assert.Equal("30na.rzv2@GMail.com", result.Data.Value);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_SendAnotherFormatEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "30na.rzv2@LocalHost");

        Assert.True(result.IsSuccess);
        Assert.Equal("30na.rzv2@LocalHost", result.Data.Value);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_CheckIsNotVerifyEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        Assert.True(result.IsSuccess);

        Assert.False(result.Data.IsVerified);
        Assert.NotNull(result.Data.VerificationKey);
        Assert.Equal("30na.rzv2@GMail.com", result.Data.Value);
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_CheckVerifyEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        var emailAddressObject = result.Data;

        var emailAddress = emailAddressObject.Value;
        var verificationKey = emailAddressObject.VerificationKey;

        var newEmailAddressObject = emailAddressObject.Verify();

        Assert.True(newEmailAddressObject.IsSuccess);
        Assert.True(newEmailAddressObject.Data.IsVerified);
        Assert.Equal(emailAddress, newEmailAddressObject.Data.Value);
        Assert.Equal(verificationKey, newEmailAddressObject.Data.VerificationKey);
      
        var successMessage = Successes.EmailAddressVerified;
        Assert.Equal(successMessage, newEmailAddressObject.Successes[0]);
        Assert.Empty(newEmailAddressObject.Errors);
        Assert.Single(newEmailAddressObject.Successes);
    }

    [Fact]
    public void Create_CheckVerifyEmailAddressAlreadyVerified_ResultIsNotSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        var emailAddressObject = result.Data;

        var newResult = emailAddressObject.Verify();

        var newNewResult = newResult.Data.Verify();

        var errorMessage = Errors.EmailAddressAlreadyVerified;

        Assert.False(newNewResult.IsSuccess);
        Assert.Equal(errorMessage, newNewResult.Errors[0]);
        Assert.Single(newNewResult.Errors);
        Assert.Empty(newNewResult.Successes);
    }

    [Fact]
    public void Create_CheckVerifyByKeyEmailAddress_ResultIsSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        var emailAddressObject = result.Data;

        var emailAddress = emailAddressObject.Value;
        var verificationKey = emailAddressObject.VerificationKey;

        var newEmailAddressObject = emailAddressObject.VerifyByKey(verificationKey);

            
        Assert.True(newEmailAddressObject.IsSuccess);
        Assert.True(newEmailAddressObject.Data.IsVerified);
        Assert.Equal(emailAddress, newEmailAddressObject.Data.Value);
        Assert.Equal(verificationKey, newEmailAddressObject.Data.VerificationKey);
          
        var successMessage = Successes.EmailAddressVerified;
        Assert.Equal(successMessage, newEmailAddressObject.Successes[0]);
        Assert.Empty(newEmailAddressObject.Errors);
        Assert.Single(newEmailAddressObject.Successes);
    }

    [Fact]
    public void Create_CheckVerifyByKeyEmailAddressInvalidVerificationKey_ResultIsNotSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        var emailAddressObject = result.Data;

        var newEmailAddressObject = emailAddressObject.VerifyByKey("abcde");

           
        Assert.False(newEmailAddressObject.IsSuccess);

        var errorMessage = Errors.InvalidVerificationKey;

        Assert.Equal(errorMessage.CleanString(), newEmailAddressObject.Errors[0]);
        Assert.Single(newEmailAddressObject.Errors);
        Assert.Empty(newEmailAddressObject.Successes);
    }

    [Fact]
    public void Create_CheckVerifyByKeyEmailAddressAlreadyVerified_ResultIsNotSuccess()
    {
        var result = EmailAddress.Create(value: "  30na.rzv2@GMail.com  ");

        var emailAddressObject = result.Data;

        var verificationKey = emailAddressObject.VerificationKey;

        var newResult = emailAddressObject.VerifyByKey(verificationKey);

        var newNewResult = newResult.Data.VerifyByKey(verificationKey: verificationKey);

        var errorMessage = Errors.EmailAddressAlreadyVerified;

        Assert.False(newNewResult.IsSuccess);
        Assert.Equal( errorMessage,  newNewResult.Errors[0]);
        Assert.Single(newNewResult.Errors);
        Assert.Empty(newNewResult.Successes);
    }
}
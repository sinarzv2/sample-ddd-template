using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Companies.ValueObjects;

namespace Domain.Test.Aggregates.Companies.ValueObjects;

public class NationalIdentityTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_SendParameterNull_ResultIsNotSuccess(string? value)
    {
        var result = NationalIdentity.Create(value);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.NationalIdentity);

        Assert.Equal(errorMessage, result.Errors[0]);
    }



    [Fact]
    public void Create_SendCorrectValue_ResultIsSuccess()
    {
        var value = "1234567890";

        var result = NationalIdentity.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal("1234567890",  result.Data.Value);
    }

    [Fact]
    public void Create_SendCorrectValueWIthSpace_ResultIsSuccess()
    {
        var value = "  1234567890  ";

        var result = NationalIdentity.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal("1234567890", result.Data.Value);
    }

    [Fact]
    public void Create_SendNotFixLength_ResultIsNotSuccess()
    {
        var value = "  12345  ";

        var result = NationalIdentity.Create(value);

        Assert.False(condition: result.IsSuccess);

        var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.NationalIdentity, NationalIdentity.FixLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
    }

    [Fact]
    public void Create_SendWrongValue_ResultIsNotSuccess()
    {
        var value = "12345abcde";

        var result = NationalIdentity.Create(value);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.NationalIdentity);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
    }
}
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Companies.ValueObjects;

namespace Domain.Test.Aggregates.Companies.ValueObjects;

public class CompanyNameTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("       ")]
    public void Create_SendParameterNull_ResultIsNotSuccess(string? value)
    {
        var result = CompanyName.Create(value: value);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.CompanyName);

        Assert.Equal(errorMessage, result.Errors[0]);
    }


    [Fact]
    public void Create_SendCorrectValue_ResultIsSuccess()
    {
        var value = "  Company 1   ";

        var result = CompanyName.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal("Company 1", result.Data.Value);
    }

    [Fact]
    public void Create_SendCorrectValueWithSpace_ResultIsSuccess()
    {
        var value = "  Company     1   ";

        var result = CompanyName.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Equal("Company 1", result.Data.Value);
    }

    [Fact]
    public void Create_SendMoreThanMaxLength_ResultIsNotSuccess()
    {
        var value = "Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1Company1";

        var result = CompanyName.Create(value);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.MaxLength, DataDictionary.CompanyName, CompanyName.MaxLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
    }
}

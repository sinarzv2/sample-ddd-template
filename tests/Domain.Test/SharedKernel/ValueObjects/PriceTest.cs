using Common.Resources.Messages;
using Common.Resources;
using Common.Utilities;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.SharedKernel.ValueObjects;

public class PriceTest
{
    [Theory]
    [InlineData(Price.Maximum + 1)]
    [InlineData(Price.Minimum - 1)]
    public void Create_SendOutOfRange_ResultIsNotSuccess(int value)
    {
        var result = Price.Create(value);
        var errorMessage = string.Format(Validations.Range, DataDictionary.Price, Price.Minimum, Price.Maximum);

        Assert.False(result.IsSuccess);
        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData(Price.Maximum)]
    [InlineData(Price.Minimum)]
    public void Create_SendInRange_ResultIsSuccess(int value)
    {
        var result = Price.Create(value);

        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void OperatorPlus_Sum2Price_ResultIsSuccess()
    {
        const int number1 = 1000;
        const int number2 = 2000;
        var price1 = Price.Create(number1);
        var price2 = Price.Create(number2);
        var sumPrice = price1.Data + price2.Data;
        var sumValue = number1 + number2;

        Assert.Equal(sumPrice.Data.Value, sumValue);

    }

    [Fact]
    public void Operatorminus_minus2Price_ResultIsSuccess()
    {
        const int number1 = 3000;
        const int number2 = 2000;
        var price1 = Price.Create(number1);
        var price2 = Price.Create(number2);
        var sumPrice = price1.Data - price2.Data;
        var sumValue = number1 - number2;

        Assert.Equal(sumPrice.Data.Value, sumValue);

    }
}
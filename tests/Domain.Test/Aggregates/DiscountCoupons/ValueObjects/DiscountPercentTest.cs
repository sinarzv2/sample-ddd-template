using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.DiscountCoupons.ValueObjects;

namespace Domain.Test.Aggregates.DiscountCoupons.ValueObjects;

public class DiscountPercentTest
{
    [Fact]
    public void Create_SendNullValue_resultIsNotSuccess()
    {
        var result = DiscountPercent.Create(null);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

        Assert.Equal(errorMessage, result.Errors[0]);

        Assert.Single(result.Errors);
    }

    [Theory]
    [InlineData(-1)]
    [InlineData(101)]
    public void Create_SendOutOfRangeValue_resultIsNotSuccess(int value)
    {
        var result = DiscountPercent.Create(value);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Range, DataDictionary.DiscountPercent, DiscountPercent.Minimum, DiscountPercent.Maximum);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
    }

   

    [Fact]
    public void Create_SendCorrectValue_resultIsSuccess()
    {
        var result = DiscountPercent.Create(50);

        Assert.True(result.IsSuccess);
        Assert.Equal(50, result.Data.Value);
    }
}
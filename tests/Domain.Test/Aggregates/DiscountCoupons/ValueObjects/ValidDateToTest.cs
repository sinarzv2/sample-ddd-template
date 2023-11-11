using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.DiscountCoupons.ValueObjects;

namespace Domain.Test.Aggregates.DiscountCoupons.ValueObjects;

public class ValidDateToTest
{
    [Fact]
    public void Create_SendNullValue_resultIsNotSuccess()
    {
        var result = ValidDateTo.Create(null);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

        Assert.Equal(errorMessage, result.Errors[0]);

        Assert.Single(result.Errors);
    }

    [Fact]
    public void Create_SendLessThanCurrentDate_resultIsNotSuccess()
    {
        var result = ValidDateTo.Create(DateTime.Now.AddDays(-1));

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_FieldValue, DataDictionary.ValidDateTo, DataDictionary.CurrentDate);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
    }

    [Fact]
    public void Create_SendCorrectDate_resultIsSuccess()
    {
        var result = ValidDateTo.Create(DateTime.Now);

        Assert.True(result.IsSuccess);
        Assert.Equal(DateTime.Now.Date, result.Data.Value);
    }

    [Fact]
    public void Create_SendMoreThanCurrentDate_resultIsSuccess()
    {
        var result = ValidDateTo.Create(DateTime.Now.AddDays(1));

        Assert.True(result.IsSuccess);
        Assert.Equal(DateTime.Now.AddDays(1).Date, result.Data.Value);
    }
}
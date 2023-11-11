using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.DiscountCoupons;

namespace Domain.Test.Aggregates.DiscountCoupons;

public class DiscountCouponTest
{
    [Fact]
    public void Create_SendAllParametersNull_Return3Errors()
    {
        var result = DiscountCoupon.Create(null, null, null);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

        Assert.Equal(errorMessage, result.Errors[0]);
      
        errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

        Assert.Equal(errorMessage, result.Errors[1]);
     
        errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

        Assert.Equal(errorMessage, result.Errors[2]);
       
        Assert.Equal(expected: 3, result.Errors.Count);
    }

    [Fact]
    public void Create_ValidDateToGraterThanValidDateFrom_ResultIsNotSuccess()
    {
        var result = DiscountCoupon.Create(50, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1));

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_TwoFields, DataDictionary.ValidDateTo, DataDictionary.ValidDateFrom);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
    }

    [Fact]
    public void Create_SendCorrectValues_ResultIsSuccess()
    {
        var result = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        Assert.True(result.IsSuccess);
        Assert.Equal(50, result.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, result.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(2).Date, result.Data.ValidDateTo.Value);
    }

    [Fact]
    public void Create_ValidDateFromEqualValidDateTo_ResultIsNotSuccess()
    {
        var result = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(1));

        Assert.True(result.IsSuccess);
        Assert.Equal(50, result.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, result.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, result.Data.ValidDateTo.Value);
    }

    [Fact]
    public void Update_SendAllParametersNull_Return3Errors()
    {
        var mainResult = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        Assert.True(mainResult.IsSuccess);
        Assert.Equal(50, mainResult.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, mainResult.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(2).Date, mainResult.Data.ValidDateTo.Value);

        var discountCoupon = mainResult.Data;

        var result = discountCoupon.Update(null, null, null);

        Assert.False(result.IsSuccess);
       
        var errorMessage = string.Format(Validations.Required, DataDictionary.DiscountPercent);

        Assert.Equal(errorMessage, result.Errors[0]);
  
        errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateFrom);

        Assert.Equal(errorMessage, result.Errors[1]);
      
        errorMessage = string.Format(Validations.Required, DataDictionary.ValidDateTo);

        Assert.Equal(errorMessage, result.Errors[2]);
       
        Assert.Equal(3, result.Errors.Count);
    }

    [Fact]
    public void Update_ValidDateToGraterThanValidDateFrom_ResultIsNotSuccess()
    {
        var mainResult = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        Assert.True(mainResult.IsSuccess);
        Assert.Equal(50, mainResult.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, mainResult.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(2).Date, mainResult.Data.ValidDateTo.Value);

        var discountCoupon = mainResult.Data;

        var result = discountCoupon.Update(70, DateTime.Now.AddDays(2), DateTime.Now.AddDays(1));

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_TwoFields, DataDictionary.ValidDateTo, DataDictionary.ValidDateFrom);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);

        Assert.Single(result.Errors);
    }

    [Fact]
    public void Update_SendCorrectValues_ResultIsSuccess()
    {
        var mainResult = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        Assert.True(mainResult.IsSuccess);
        Assert.Equal(50, mainResult.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, mainResult.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(2).Date, mainResult.Data.ValidDateTo.Value);

        var discountCoupon = mainResult.Data;

        var result = discountCoupon.Update(70, DateTime.Now.AddDays(3), DateTime.Now.AddDays(4));

        Assert.True(result.IsSuccess);
        Assert.Equal(70, discountCoupon.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(3).Date, discountCoupon.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(4).Date, discountCoupon.ValidDateTo.Value);
    }

    [Fact]
    public void Update_ValidDateFromEqualValidDateTo_ResultIsNotSuccess()
    {
        var mainResult = DiscountCoupon.Create(50, DateTime.Now.AddDays(1), DateTime.Now.AddDays(2));

        Assert.True(mainResult.IsSuccess);
        Assert.Equal(50, mainResult.Data.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(1).Date, mainResult.Data.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(2).Date, mainResult.Data.ValidDateTo.Value);

        var discountCoupon = mainResult.Data;

        var result = discountCoupon.Update(70, DateTime.Now.AddDays(4), DateTime.Now.AddDays(4));

        Assert.True(result.IsSuccess);
        Assert.Equal(expected: 70, discountCoupon.DiscountPercent.Value);
        Assert.Equal(DateTime.Now.AddDays(4).Date, discountCoupon.ValidDateFrom.Value);
        Assert.Equal(DateTime.Now.AddDays(4).Date, discountCoupon.ValidDateTo.Value);
    }
}
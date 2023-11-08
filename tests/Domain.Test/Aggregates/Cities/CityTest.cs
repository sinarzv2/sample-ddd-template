using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Cities;
using Domain.Aggregates.Provinces;

namespace Domain.Test.Aggregates.Cities;

public class CityTest
{
    [Theory]
    [InlineData(null, null)]
    [InlineData(null, "")]
    [InlineData(null, "     ")]
    public void Create_SendProviceAndNameNull_ReturnTwoErrors(Province? province, string? name)
    {
        var result = City.Create(province, name);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Province);

        Assert.Equal(errorMessage, result.Errors[0]);

        errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[1]);

        Assert.Empty(result.Successes);
        Assert.Equal(2, result.Errors.Count);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("     ")]
    public void Create_SendNameNull_ReturnOneError(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var result = City.Create(provinceResult.Data, name);

     
        Assert.False(result.IsSuccess);
     
        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[0]);
    
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData("My City")]
    [InlineData("    My City    ")]
    [InlineData("    My    City    ")]
    public void Create_SendCorrectValues_ResultIsSuccess(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var result = City.Create(provinceResult.Data, name);

        
        Assert.True(result.IsSuccess);
       
        Assert.Equal("My City", result.Data.Name.Value);
        Assert.Equal(provinceResult.Data,  result.Data.Province);
       
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("      ")]
    public void Update_SendNameNull_ReturnOneError(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var cityResult = City.Create(provinceResult.Data, "My City");

        var result = cityResult.Data.Update(name);

     
        Assert.False(result.IsSuccess);
      
        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[0]);
        
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
        
    }


    [Theory]
    [InlineData("New My City")]
    [InlineData("   New My City   ")]
    [InlineData("   New   My   City   ")]
    public void Update_SendCorrectValues_ResultIsSuccess(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var cityResult = City.Create(provinceResult.Data, "My City");

        var result = cityResult.Data.Update(name);

       
        Assert.True(result.IsSuccess);
  
        Assert.Equal("New My City", cityResult.Data.Name.Value);

        Assert.Equal(provinceResult.Data, cityResult.Data.Province);
       
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }
}
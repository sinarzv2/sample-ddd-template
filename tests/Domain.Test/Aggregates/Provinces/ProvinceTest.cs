using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Provinces;

namespace Domain.Test.Aggregates.Provinces;

public class ProvinceTest
{
    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Create_SendNameNull_ReturnError(string? name)
    {
        var result = Province.Create(name);


        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[0]);
  
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }



    [Theory]
    [InlineData("Province 1")]
    [InlineData("   Province 1   ")]
    [InlineData("   Province   1   ")]
    public void Create_SendCorrectValue_ResultIsSuccess(string? name)
    {
        var result = Province.Create(name);

 
        Assert.True(result.IsSuccess);

        Assert.Equal("Province 1", result.Data.Name.Value);

        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void Create_CreateProvinceWithId_ResultIsSuccess()
    {
        var id = Guid.NewGuid();
        var result = Province.Create("  Province  ", id);

        
        Assert.True(result.IsSuccess);

        Assert.Equal("Province", result.Data.Name.Value);
        Assert.Equal(id, result.Data.Id);

        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void AddCity_AddCityWithNameNull_ReturnError(string? cityName)
    {
        var provinceResult = Province.Create("Province");

        var result = provinceResult.Data.AddCity(cityName);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal( errorMessage, result.Errors[0]);
       
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData("City 1")]
    [InlineData("   City 1   ")]
    [InlineData("   City   1   ")]
    public void AddCity_SendCorrectValue_ResultIsSuccess(string? cityName)
    {
        var provinceResult = Province.Create("Province");

        var result = provinceResult.Data.AddCity(cityName);

    
        Assert.True(result.IsSuccess);

        Assert.Equal("City 1", result.Data.Name.Value);
       
        Assert.Single(provinceResult.Data.Cities);
     
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }



    [Fact]
    public void AddCity_AddTwoCities_CollectionHasTwoCities()
    {
        var provinceResult = Province.Create("Province");

        var cityResult = provinceResult.Data.AddCity(cityName: "   City   1   ");

        var result = provinceResult.Data.AddCity(cityName: "   City   2   ");

    
        Assert.True(result.IsSuccess);
        
        Assert.Equal("City 1", cityResult.Data.Name.Value);
        Assert.Equal("City 2", result.Data.Name.Value);

        
        Assert.Equal(2, provinceResult.Data.Cities.Count);
       
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Fact]
    public void AddCity_AddTwoCitiesWithSameName_ReturnRepetitiveError()
    {
        var provinceResult = Province.Create("Province");

        provinceResult.Data.AddCity(cityName: "   City   1   ");

        var result = provinceResult.Data.AddCity(cityName: "     City     1     ");


        Assert.False(result.IsSuccess);
     
        var errorMessage = string.Format(Validations.Repetitive, DataDictionary.CityName);

        Assert.Equal(errorMessage, result.Errors[0]);

        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void RemoveCity_RemoveCityWithNameNull_ReturnError(string? cityName)
    {
        var provinceResult = Province.Create("Province");

        var result = provinceResult.Data.RemoveCity(cityName);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[0]);
  
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Fact]
    public void RemoveCity_TryToRemoveWorngCity_ReturnNotFoundError()
    {
        var provinceResult = Province.Create("Province");

        var result = provinceResult.Data.RemoveCity("   City   1   ");

        
        Assert.False(result.IsSuccess);
  
        var errorMessage = string.Format(Validations.NotFound, DataDictionary.City);

        Assert.Equal(errorMessage, result.Errors[0]);
       
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
        
    }

    [Fact]
    public void RemoveCity_TryToRemoveNotExistCity_ReturnNotFoundError()
    {
        var provinceResult = Province.Create("Province");

        provinceResult.Data.AddCity("   City   1   ");

        var result = provinceResult.Data.RemoveCity("   City   2   ");


        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.NotFound, DataDictionary.City);

        Assert.Equal(errorMessage, actual: result.Errors[0]);
     
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
      

        Assert.Single(provinceResult.Data.Cities);
      
    }

    [Fact]
    public void RemoveCity_RemoveCorrectCity_ResultIsSuccess()
    {
        var provinceResult = Province.Create("Province");

        provinceResult.Data.AddCity("   City   1   ");

        var result = provinceResult.Data.RemoveCity("     City     1     ");

       
        Assert.True(result.IsSuccess);
    
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
  
        Assert.Empty(provinceResult.Data.Cities);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("    ")]
    public void Update_SendNameNull_ReturnError(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var result = provinceResult.Data.Update(name);

     
        Assert.False(result.IsSuccess);
        
        var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

        Assert.Equal(errorMessage, result.Errors[0]);
       
        Assert.Single(result.Errors);
        Assert.Empty(result.Successes);
    }


    [Theory]
    [InlineData("New My Province")]
    [InlineData("   New My Province   ")]
    [InlineData("   New   My   Province   ")]
    public void Update_SendCorrectValue_ResultIsSuccess(string? name)
    {
        var provinceResult = Province.Create("My Province");

        var result = provinceResult.Data.Update(name);

    
        Assert.True(result.IsSuccess);
    
        Assert.Empty(provinceResult.Data.Cities);

        Assert.Equal("New My Province", provinceResult.Data.Name.Value);
       
        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }

}
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.Aggregates.Identity;
using Domain.Aggregates.Identity.ValueObjects;
using Domain.SharedKernel.Enumerations;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Test.Aggregates.Identity;

public class UserTest
{
    [Fact]
    public void Create_SendAllParameterNull_ResultIsNotSuccess()
    {
        var result = User.Create
            (username: null, password: null, emailAddress: null,
             gender: null, firstName: null, lastName: null, phoneNumber: null, birthDate: null);

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.Required, DataDictionary.Username);

        Assert.Equal(errorMessage, result.Errors[0]);
    
        errorMessage = string.Format(Validations.Required, DataDictionary.Password);

        Assert.Equal(errorMessage, result.Errors[1]);
       
        errorMessage = string.Format(Validations.Required, DataDictionary.EmailAddress);

        Assert.Equal(errorMessage, result.Errors[2]);
       
        errorMessage = string.Format(Validations.Required, DataDictionary.PhoneNumber);

        Assert.Equal(errorMessage, result.Errors[3]);

        errorMessage = string.Format(Validations.Required, DataDictionary.Birthdate);

        Assert.Equal(errorMessage, result.Errors[4]);

        errorMessage = string.Format(Validations.Required, DataDictionary.Gender);

        Assert.Equal(errorMessage, result.Errors[5]);

        errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

        Assert.Equal(errorMessage, result.Errors[6]);

        errorMessage = string.Format(Validations.Required, DataDictionary.LastName);

        Assert.Equal(errorMessage, result.Errors[7]);
      
        Assert.Empty(result.Successes);
        Assert.Equal(8, result.Errors.Count);
    }

    [Fact]
    public void Create_SendAllParameterEmpty_ResultIsNotSuccess()
    {
        var result = User.Create
            (username: string.Empty, password: string.Empty, emailAddress: string.Empty,
             gender: 10, firstName: string.Empty, lastName: string.Empty, phoneNumber:string.Empty, birthDate: DateTime.MaxValue);

    
        Assert.False(result.IsSuccess);
   
        var errorMessage = string.Format(Validations.Required, DataDictionary.Username);

        Assert.Equal(errorMessage, result.Errors[0]);

        errorMessage = string.Format(Validations.Required, DataDictionary.Password);

        Assert.Equal(errorMessage, result.Errors[1]);
  
        errorMessage = string.Format(Validations.Required, DataDictionary.EmailAddress);

        Assert.Equal(errorMessage, result.Errors[2]);

        errorMessage = string.Format(Validations.Required, DataDictionary.PhoneNumber);

        Assert.Equal(errorMessage, result.Errors[3]);

        errorMessage = string.Format(Validations.Range, DataDictionary.Birthdate, BirthDate.MinValue, BirthDate.MaxValue);

        Assert.Equal(errorMessage.CleanString(), result.Errors[4]);

        errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

        Assert.Equal(errorMessage.CleanString(), result.Errors[5]);

        errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

        Assert.Equal(errorMessage, result.Errors[6]);
       
        errorMessage = string.Format(Validations.Required, DataDictionary.LastName);

        Assert.Equal(errorMessage, result.Errors[7]);
     
        Assert.Empty(result.Successes);
        Assert.Equal(8, result.Errors.Count);
    }

    [Fact]
    public void Create_SendIncorrectValue_ResultIsNotSuccess()
    {
        var result = User.Create
            (username: "  AliReza  ", password: "  12345  ", emailAddress: "  AliReza  ", 
                gender: 10, firstName: "     ", lastName: "     ", birthDate: DateTime.MinValue, phoneNumber: "45154");

        Assert.False(result.IsSuccess);

        var errorMessage = string.Format(Validations.MinLength, DataDictionary.Username, Username.MinLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[0]);
 
        errorMessage = string.Format(Validations.MinLength, DataDictionary.Password, Username.MinLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[1]);
      
        errorMessage = string.Format(Validations.RegularExpression, DataDictionary.EmailAddress);

        Assert.Equal(errorMessage.CleanString(), result.Errors[2]);

        errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.PhoneNumber, PhoneNumber.FixLength);

        Assert.Equal(errorMessage.CleanString(), result.Errors[3]);

        errorMessage = string.Format(Validations.Range, DataDictionary.Birthdate, BirthDate.MinValue, BirthDate.MaxValue);

        Assert.Equal(errorMessage.CleanString(), result.Errors[4]);

        errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

        Assert.Equal(errorMessage.CleanString(), result.Errors[5]);
       
        errorMessage = string.Format(Validations.Required, DataDictionary.FirstName);

        Assert.Equal(errorMessage, result.Errors[6]);
       
        errorMessage = string.Format(Validations.Required, DataDictionary.LastName);

        Assert.Equal(errorMessage, result.Errors[7]);
    
        Assert.Empty(result.Successes);
        Assert.Equal(8, result.Errors.Count);
    }


    [Fact]
    public void Create_SendCorrectValue_ResultIsSuccess()
    {
        var result = User.Create
            (username: "  AliRezaAlavi  ", password: "  1234512345  ",
            emailAddress: "  AliReza@GMail.com  ",
            gender: Gender.Male.Value,
            firstName: "  Ali   Reza  ", lastName: "  Alavi   Asl  ",
            phoneNumber:"09354831413", birthDate:new DateTime(1991,05,29));

        Assert.True(result.IsSuccess);
     
        Assert.Equal("AliRezaAlavi", result.Data.UserName);
        
        Assert.Equal("AliReza@GMail.com", result.Data.Email);
        
        Assert.Equal(Gender.Male, result.Data.FullName.Gender);
    
        Assert.Equal("Ali Reza", result.Data.FullName.FirstName.Value);
    
        Assert.Equal("Alavi Asl", result.Data.FullName.LastName.Value);

        Assert.Empty(result.Errors);
        Assert.Empty(result.Successes);
    }
}
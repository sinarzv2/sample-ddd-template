using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Companies.ValueObjects;

public class CompanyName : ValueObject
{
    public const int MaxLength = 10;

    public static CompanyName Default = new(string.Empty);

    public static FluentResult<CompanyName> Create(string? value)
    {
        var result = new FluentResult<CompanyName>();

        value = value.Fix();

        if (value is null)
        {
            var errorMessage = string.Format(Validations.Required, DataDictionary.CompanyName);

            result.AddError(errorMessage);

            return result;
        }

        if (value.Length > MaxLength)
        {
            var errorMessage = string.Format(Validations.MaxLength, DataDictionary.CompanyName, MaxLength);

            result.AddError(errorMessage);

            return result;
        }

        var returnValue = new CompanyName(value: value);

        result.SetData(returnValue);

        return result;
    }


    private CompanyName()
    {
    }

    protected CompanyName(string value) : this()
    {
        Value = value;
    }

    public string Value { get; } = string.Empty;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    public override string ToString()
    {
        return Value;
    }
}
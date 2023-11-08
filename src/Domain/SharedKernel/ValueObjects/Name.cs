using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects;

public class Name : ValueObject
{
    public const int MaxLength = 50;

    public static Name Default = new(string.Empty);

    public static FluentResult<Name> Create(string? value)
    {
        var result = new FluentResult<Name>();

        value = value.Fix();

        if (value is null)
        {
            var errorMessage = string.Format(Validations.Required, DataDictionary.Name);

            result.AddError(errorMessage);

            return result;
        }

        if (value.Length > MaxLength)
        {
            var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Name, MaxLength);

            result.AddError(errorMessage);

            return result;
        }

        var returnValue = new Name(value: value);

        result.SetData(returnValue);

        return result;
    }


    private Name()
    {
    }

    private Name(string value) : this()
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
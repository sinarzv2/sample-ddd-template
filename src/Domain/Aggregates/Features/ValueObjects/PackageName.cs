using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Features.ValueObjects;

public class PackageName : ValueObject
{
    public const int MaxLength = 100;

    public static PackageName Default = new(string.Empty);

    public static FluentResult<PackageName> Create(string? value)
    {
        var result = new FluentResult<PackageName>();

        value = value.Fix();

        if (value is null)
        {
            var errorMessage = string.Format(Validations.Required, DataDictionary.PackageName);

            result.AddError(errorMessage);

            return result;
        }

        if (value.Length > MaxLength)
        {
            var errorMessage = string.Format(Validations.MaxLength, DataDictionary.PackageName, MaxLength);

            result.AddError(errorMessage);

            return result;
        }

        var returnValue = new PackageName(value);

        result.SetData(returnValue);

        return result;
    }


    private PackageName()
    {
    }

    protected PackageName(string value) : this()
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
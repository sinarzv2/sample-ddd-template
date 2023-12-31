﻿using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Provinces;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Cities;

public sealed class City : AggregateRoot
{
    public static FluentResult<City> Create(Province? province, string? name)
    {
        var result = new FluentResult<City>();

        if (province is null)
        {
            var errorMessage = string.Format(Validations.Required, DataDictionary.Province);

            result.AddError(errorMessage);
        }

        var nameResult = Name.Create(name);

        result.AddErrors(nameResult.Errors);

        if (result.Errors.Any())
        {
            return result;
        }

        var returnValue = new City(province!, nameResult.Data);

        result.SetData(returnValue);

        return result;
    }

    private City()
    {
    }

    private City(Province province, Name name) : this()
    {
        Name = name;
        Province = province;
    }

    public Name Name { get; private set; } = Name.Default;

    public Guid ProvinceId { get; init; }

    public Province Province { get; init; } = default!;

    public FluentResult Update(string? name)
    {
        var result = Create(Province, name);

        if (result.Errors.Any())
        {
            return result;
        }

        Name = result.Data.Name;

        return result;
    }
}
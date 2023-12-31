﻿using Common.Models;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace Domain.Aggregates.Identity;

public sealed class Role : IdentityRole<Guid>, IEntity
{


    public static FluentResult<Role> Create(string? name)
    {
        var result = new FluentResult<Role>();

        var nameResult = SharedKernel.ValueObjects.Name.Create(name);

        result.AddErrors(nameResult.Errors);

        if (result.IsSuccess == false)
        {
            return result;
        }

        var returnValue = new Role(nameResult.Data);

        result.SetData(returnValue);

        return result;

    }

    private Role()
    {
    }
    private Role(Name name) : this()
    {
        Name = name.Value;
        NormalizedName = name.Value.ToUpper();
    }

}
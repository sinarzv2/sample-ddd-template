﻿using Common.Models;
using Domain.Aggregates.Identity.Events;
using Domain.Aggregates.Identity.ValueObjects;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Domain.Aggregates.Identity;

public sealed class User : IdentityUser<Guid>, IAggregateRoot
{

    [JsonIgnore]
    private readonly List<IDomainEvent> _domainEvents;

    [JsonIgnore]
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents;


    public static FluentResult<User> Create(string? username, string? password, string? emailAddress,
        string? phoneNumber, int? gender, string? firstName, string? lastName, DateTime? birthDate)
    {
        var result = new FluentResult<User>();

        var usernameResult = Username.Create(username);

        result.AddErrors(usernameResult.Errors);

        var passwordResult = Password.Create(password);

        result.AddErrors(passwordResult.Errors);

        var emailAddressResult = EmailAddress.Create(emailAddress);

        result.AddErrors(emailAddressResult.Errors);

        var phoneNumberResult = SharedKernel.ValueObjects.PhoneNumber.Create(phoneNumber);

        result.AddErrors(phoneNumberResult.Errors);

        var birthdateResult = BirthDate.Create(birthDate);

        result.AddErrors(birthdateResult.Errors);

        var fullNameResult = FullName.Create(gender, firstName, lastName);

        result.AddErrors(fullNameResult.Errors);

        if (result.IsSuccess == false)
        {
            return result;
        }


        var returnValue =
            new User(usernameResult.Data,
                emailAddressResult.Data, phoneNumberResult.Data,
                fullNameResult.Data, birthdateResult.Data, true);

            

        result.SetData(returnValue);
        return result;
    }
    private User()
    {
        _domainEvents = new List<IDomainEvent>();
    }

    private User(Username username, EmailAddress emailAddress, PhoneNumber phoneNumber, FullName fullName, BirthDate birthDate, bool isActive) : this()
    {
        FullName = fullName;
        UserName = username.Value;
        Email = emailAddress.Value;
        PhoneNumber = phoneNumber.Value;
        BirthDate = birthDate;
        IsActive = isActive;
        SetNewRefreshToken();
    }

    public FullName FullName { get; init; } = FullName.Default;
    public BirthDate BirthDate { get; init; } = BirthDate.Default;
    public bool IsActive { get; init; }
    public RefreshToken RefreshToken { get; private set; } = RefreshToken.Create().Data;

    public FluentResult ChangePassword(string newPassword,string hashPassword)
    {
        var result = new FluentResult();

        var newPasswordResult = Password.Create(newPassword);

        if (newPasswordResult.Errors.Any())
        {
            result.AddErrors(newPasswordResult.Errors);

            return result;
        }

        PasswordHash = hashPassword;

        _domainEvents.Add(new UserPasswordChangedEvent(FullName, Email));

        return result;
    }

    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

    public FluentResult SetNewRefreshToken()
    {
        var result = new FluentResult();
        var refreshTokenResult = RefreshToken.Create();

        if (refreshTokenResult.Errors.Any())
        {
            result.AddErrors(refreshTokenResult.Errors);

            return result;
        }
        RefreshToken = refreshTokenResult.Data;
        return result;
    }


    public void RevokeToken()
    {
        RefreshToken.ClearToken();
    }
}
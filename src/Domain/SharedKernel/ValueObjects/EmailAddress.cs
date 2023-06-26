using System.Net.Mail;
using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.SharedKernel.ValueObjects
{
    public class EmailAddress : ValueObject
    {
       
        public const int MaxLength = 250;

        public const int VerificationKeyFixLength = 32;

        public static EmailAddress Default = new(string.Empty);

        public static FluentResult<EmailAddress> Create(string? value)
        {
            var result = new FluentResult<EmailAddress>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.EmailAddress);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.EmailAddress, MaxLength);

                result.AddError(errorMessage);

                return result;
            }

            try
            {
                _ = new MailAddress(value).Address;
            }
            catch
            {
                var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.EmailAddress);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new EmailAddress(value: value);

            result.SetData(returnValue);

            return result;
        }

        public string GetVerificationKey()
        {
            var result = Guid.NewGuid().ToString().Replace("-", string.Empty);

            return result;
        }

        private EmailAddress()
        {
        }

        private EmailAddress(string value) : this()
        {
            Value = value;
            IsVerified = false;
            VerificationKey = GetVerificationKey();

        }

        private EmailAddress
            (string value, bool isVerified, string verificationKey) : this(value: value)
        {
            IsVerified = isVerified;
            VerificationKey = verificationKey;
        }

        public string Value { get; } = string.Empty;

        public bool IsVerified { get; } 

        public string VerificationKey { get; } = string.Empty;

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value;
            yield return IsVerified;
            yield return VerificationKey;
        }

        public FluentResult<EmailAddress> Verify()
        {
            var result = new FluentResult<EmailAddress>();

            if (IsVerified)
            {
                result.AddError(Errors.EmailAddressAlreadyVerified);

                return result;
            }

            var newObject = new EmailAddress(Value, true, VerificationKey);

            result.SetData(newObject);

            return result;
        }

        public FluentResult<EmailAddress> VerifyByKey(string verificationKey)
        {
            var result = new FluentResult<EmailAddress>();

            if (string.CompareOrdinal(VerificationKey, verificationKey) != 0)
            {
                result.AddError(Errors.InvalidVerificationKey);

                return result;
            }

            result = Verify();

            return result;
        }

        public override string ToString()
        {
            return Value;
        }
    }
}

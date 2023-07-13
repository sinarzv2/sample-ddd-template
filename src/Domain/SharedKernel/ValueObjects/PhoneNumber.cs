using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;
using System.Text.RegularExpressions;

namespace Domain.SharedKernel.ValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public const int FixLength = 11;

        public const int VerificationKeyFixLength = 6;

        public const string RegularExpression = @"09\d{9}";

        public static PhoneNumber Default = new(string.Empty);

        public static FluentResult<PhoneNumber> Create(string? value)
        {
            var result = new FluentResult<PhoneNumber>();

            value = value.Fix();

            if (value is null)
            {
                var errorMessage = string.Format(Validations.Required, DataDictionary.PhoneNumber);

                result.AddError(errorMessage);

                return result;
            }

            if (value.Length != FixLength)
            {
                var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.PhoneNumber, FixLength);

                result.AddError(errorMessage);

                return result;
            }

            if (Regex.IsMatch(value, RegularExpression) == false)
            {
                var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.PhoneNumber);

                result.AddError(errorMessage);

                return result;
            }

            var returnValue = new PhoneNumber(value: value);
            result.SetData(returnValue);
            return result;
        }

        public string GetVerificationKey()
        {
            string result =
                Guid.NewGuid()
                .ToString().Replace("-", string.Empty)
                .Substring(startIndex: 0, length: VerificationKeyFixLength);

            return result;
        }

        private PhoneNumber()
        {
        }

        private PhoneNumber(string value) : this()
        {
            Value = value;
            IsVerified = false;
            VerificationKey = GetVerificationKey();

        }

        private PhoneNumber(string value, bool isVerified, string verificationKey) : this(value: value)
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

        public FluentResult<PhoneNumber> Verify()
        {
            var result = new FluentResult<PhoneNumber>();

            if (IsVerified)
            {
                result.AddError(Errors.PhoneNumberAlreadyVerified);

                return result;
            }

            var newObject = new PhoneNumber(Value, true, VerificationKey);

            result.SetData(newObject);

            return result;
        }

        public FluentResult<PhoneNumber> VerifyByKey(string verificationKey)
        {
            var result = new FluentResult<PhoneNumber>();

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

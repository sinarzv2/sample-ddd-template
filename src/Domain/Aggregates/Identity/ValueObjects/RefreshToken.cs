using Common.Models;
using Common.Resources.Messages;
using Common.Resources;
using Domain.SeedWork;
using System.Security.Cryptography;

namespace Domain.Aggregates.Identity.ValueObjects
{
    public class RefreshToken : ValueObject
    {
        public const int MaxLength = 44;
        public const int ExpirationRefreshTimeDays = 7;
        public static FluentResult<RefreshToken> Create()
        {
            var result = new FluentResult<RefreshToken>();

            var token = Generate();

            if (token.Length > MaxLength)
            {
                var errorMessage = string.Format(Validations.MaxLength, DataDictionary.RefreshToken, MaxLength);

                result.AddError(errorMessage);

                return result;
            }
            var refreshToken = new RefreshToken(Generate(), DateTime.Now.AddDays(ExpirationRefreshTimeDays));
            result.SetData(refreshToken);
            return result;
        }

        public string Token { get; init; } = string.Empty;
        public DateTime ExpiryTime { get; init; }

        private RefreshToken()
        {
        }

        private RefreshToken(string token, DateTime expiryTime) : this()
        {
            Token = token;
            ExpiryTime = expiryTime;
        }
        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Token;
            yield return ExpiryTime;
        }

        private static string Generate()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}

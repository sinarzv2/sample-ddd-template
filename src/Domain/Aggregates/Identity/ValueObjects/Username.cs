using System.Text.RegularExpressions;
using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Identity.ValueObjects
{
	public class Username : ValueObject
	{
		
		public const int MinLength = 8;

		public const int MaxLength = 20;

		public const string RegularExpression = "^[a-zA-Z0-9_-]{8,20}$";
		

		
		public static FluentResult<Username> Create(string? value)
		{
			var result = new FluentResult<Username>();

			value = value.Fix();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.Username);

				result.AddError(errorMessage);

				return result;
			}

			if (value.Length < MinLength)
			{
				var errorMessage = string.Format(Validations.MinLength, DataDictionary.Username, MinLength);

				result.AddError(errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				var errorMessage = string.Format(Validations.MaxLength, DataDictionary.Username, MaxLength);

				result.AddError(errorMessage);

				return result;
			}

			if (Regex.IsMatch(value, RegularExpression) == false)
			{
				var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.Username);

				result.AddError(errorMessage);

				return result;
			}

			var returnValue = new Username(value);

			result.SetData(returnValue);

			return result;
		}

        private Username()
        {
		}

		private Username(string value) : this()
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
}
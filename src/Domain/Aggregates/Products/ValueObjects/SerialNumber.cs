using System.Text.RegularExpressions;
using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Products.ValueObjects
{
	public class SerialNumber : ValueObject
	{
		public const int FixLength = 20;

		public const string RegularExpression = "^[0-9]{20}$";

        public static SerialNumber Default = new(string.Empty);

        public static FluentResult<SerialNumber> Create(string? value)
		{
			var result = new FluentResult<SerialNumber>();

			value = value.Fix();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.SerialNumber);

				result.AddError(errorMessage);

				return result;
			}

			if (value.Length != FixLength)
			{
				var errorMessage = string.Format(Validations.FixLengthNumeric, DataDictionary.SerialNumber, FixLength);

				result.AddError(errorMessage);

				return result;
			}

			if (Regex.IsMatch(value, RegularExpression) == false)
			{
				var errorMessage = string.Format(Validations.RegularExpression, DataDictionary.SerialNumber);

				result.AddError(errorMessage: errorMessage);

				return result;
			}

			var returnValue = new SerialNumber(value);

			result.SetData(returnValue);

			return result;
		}
		

		private SerialNumber()
        {
		}

		private SerialNumber(string value) : this()
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

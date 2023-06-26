using Common.Models;
using Common.Utilities;
using Domain.SeedWork;

namespace Domain.Aggregates.Features.ValueObjects
{
	public class FeatureName : ValueObject
	{
		
		public const int MaxLength = 100;
	

		
		public static FluentResult<FeatureName> Create(string value)
		{
			var result = new FluentResult<FeatureName>();

			value = value.Fix();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.FeatureName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Length > MaxLength)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.MaxLength,
					Resources.DataDictionary.FeatureName, MaxLength);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new FeatureName(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private FeatureName() : base()
		{
		}

		protected FeatureName(string value) : this()
		{
			Value = value;
		}

		public string Value { get; private set; }

		protected override
			System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			return Value;
		}
	}
}

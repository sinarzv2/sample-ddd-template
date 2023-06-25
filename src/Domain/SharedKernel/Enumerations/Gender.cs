using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.SeedWork;

namespace Domain.SharedKernel.Enumerations
{
	public class Gender : Enumeration
	{
		public const int MaxLength = 10;

		public static readonly Gender Male = new(0, DataDictionary.Male);
		public static readonly Gender Female = new(1, DataDictionary.Female);

		public static FluentResult<Gender> GetByValue(int? value)
		{
			var result = new FluentResult<Gender>();

			if (value is null)
			{
				var errorMessage = string.Format(Validations.Required, DataDictionary.Gender);

				result.AddError(errorMessage);

				return result;
			}

			var gender = FromValue<Gender>(value.Value);

			if (gender is null)
			{
				var errorMessage = string.Format(Validations.InvalidCode, DataDictionary.Gender);

				result.AddError(errorMessage);

				return result;
			}

			result.SetData(gender);

			return result;
		}


		private Gender(int value, string name) : base(value: value, name: name)
		{
		}
	}
}


namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class ValidDateTo : SharedKernel.Date
	{
		#region Static Member(s)
		public static FluentResults.Result<ValidDateTo> Create(System.DateTime? value)
		{
			var result =
				new FluentResults.Result<ValidDateTo>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.ValidDateTo);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Value.Date < SeedWork.Utility.Today)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.GreaterThanOrEqualTo_FieldValue,
					Resources.DataDictionary.ValidDateTo, Resources.DataDictionary.CurrentDate);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new ValidDateTo(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private ValidDateTo() : base()
		{
		}

		private ValidDateTo(System.DateTime? value) : base(value: value)
		{
		}
	}
}

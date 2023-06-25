namespace Domain.Aggregates.DiscountCoupons.ValueObjects
{
	public class ValidDateFrom : SharedKernel.Date
	{
		#region Static Member(s)
		public static FluentResults.Result<ValidDateFrom> Create(System.DateTime? value)
		{
			var result =
				new FluentResults.Result<ValidDateFrom>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.ValidDateFrom);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			if (value.Value.Date < SeedWork.Utility.Today)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.GreaterThanOrEqualTo_FieldValue,
					Resources.DataDictionary.ValidDateFrom, Resources.DataDictionary.CurrentDate);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new ValidDateFrom(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private ValidDateFrom() : base()
		{
		}

		private ValidDateFrom(System.DateTime? value) : base(value: value)
		{
		}
	}
}

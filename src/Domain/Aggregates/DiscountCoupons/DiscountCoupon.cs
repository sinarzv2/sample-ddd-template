namespace Domain.Aggregates.DiscountCoupons
{
	public class DiscountCoupon : SeedWork.AggregateRoot
	{
		#region Static Member(s)
		public static FluentResults.Result<DiscountCoupon> Create
			(int? discountPercent, System.DateTime? validDateFrom, System.DateTime? validDateTo)
		{
			var result =
				new FluentResults.Result<DiscountCoupon>();

			// **************************************************
			var discountPercentResult =
				ValueObjects.DiscountPercent.Create(value: discountPercent);

			result.WithErrors(errors: discountPercentResult.Errors);
			// **************************************************

			// **************************************************
			var validDateFromResult =
				ValueObjects.ValidDateFrom.Create(value: validDateFrom);

			result.WithErrors(errors: validDateFromResult.Errors);
			// **************************************************

			// **************************************************
			var validDateToResult =
				ValueObjects.ValidDateTo.Create(value: validDateTo);

			result.WithErrors(errors: validDateToResult.Errors);
			// **************************************************

			if (result.IsFailed)
			{
				return result;
			}

			if (validDateToResult.Value < validDateFromResult.Value)
			{
				string errorMessage =
					string.Format(Resources.Messages.Validations.GreaterThanOrEqualTo_TwoFields,
					Resources.DataDictionary.ValidDateTo, Resources.DataDictionary.ValidDateFrom);

				result.WithError(errorMessage);
			}

			if (result.IsFailed)
			{
				return result;
			}

			var returnValue =
				new DiscountCoupon
				(discountPercent: discountPercentResult.Value,
				validDateFrom: validDateFromResult.Value, validDateTo: validDateToResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private DiscountCoupon() : base()
		{
		}

		private DiscountCoupon
			(ValueObjects.DiscountPercent discountPercent,
			ValueObjects.ValidDateFrom validDateFrom, ValueObjects.ValidDateTo validDateTo) : this()
		{
			ValidDateTo = validDateTo;
			ValidDateFrom = validDateFrom;
			DiscountPercent = discountPercent;
		}

		public ValueObjects.ValidDateTo ValidDateTo { get; private set; }

		public ValueObjects.ValidDateFrom ValidDateFrom { get; private set; }

		public ValueObjects.DiscountPercent DiscountPercent { get; private set; }

		//public FluentResults.Result Update
		//	(int? discountPercent, System.DateTime? validDateFrom, System.DateTime? validDateTo)
		//{
		//	var result =
		//		new FluentResults.Result();

		//	// **************************************************
		//	var discountPercentResult =
		//		ValueObjects.DiscountPercent.Create(value: discountPercent);

		//	result.WithErrors(errors: discountPercentResult.Errors);
		//	// **************************************************

		//	// **************************************************
		//	var validDateFromResult =
		//		ValueObjects.ValidDateFrom.Create(value: validDateFrom);

		//	result.WithErrors(errors: validDateFromResult.Errors);
		//	// **************************************************

		//	// **************************************************
		//	var validDateToResult =
		//		ValueObjects.ValidDateTo.Create(value: validDateTo);

		//	result.WithErrors(errors: validDateToResult.Errors);
		//	// **************************************************

		//	if (result.IsFailed)
		//	{
		//		return result;
		//	}

		//	if (validDateToResult.Value < validDateFromResult.Value)
		//	{
		//		string errorMessage =
		//			string.Format(Resources.Messages.Validations.GreaterThanOrEqualTo_TwoFields,
		//			Resources.DataDictionary.ValidDateTo, Resources.DataDictionary.ValidDateFrom);

		//		result.WithError(errorMessage);
		//	}

		//	if (result.IsFailed)
		//	{
		//		return result;
		//	}

		//	ValidDateTo = validDateToResult.Value;
		//	ValidDateFrom = validDateFromResult.Value;
		//	DiscountPercent = discountPercentResult.Value;

		//	return result;
		//}

		/// <summary>
		/// DRY Pattern: Do not repeat yourself!
		/// </summary>
		public FluentResults.Result Update
			(int? discountPercent, System.DateTime? validDateFrom, System.DateTime? validDateTo)
		{
			var result = Create
				(validDateTo: validDateTo,
				validDateFrom: validDateFrom,
				discountPercent: discountPercent);

			if (result.IsFailed)
			{
				return result.ToResult();
			}

			ValidDateTo = result.Value.ValidDateTo;
			ValidDateFrom = result.Value.ValidDateFrom;
			DiscountPercent = result.Value.DiscountPercent;

			return result.ToResult();
		}
	}
}

using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.DiscountCoupons.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.DiscountCoupons
{
	public class DiscountCoupon : AggregateRoot
	{
		public static FluentResult<DiscountCoupon> Create(int? discountPercent, DateTime? validDateFrom, DateTime? validDateTo)
		{
			var result = new FluentResult<DiscountCoupon>();

			var discountPercentResult = DiscountPercent.Create(value: discountPercent);

			result.AddErrors(discountPercentResult.Errors);

			var validDateFromResult = ValidDateFrom.Create(value: validDateFrom);

			result.AddErrors(validDateFromResult.Errors);

			var validDateToResult = ValidDateTo.Create(value: validDateTo);

			result.AddErrors(validDateToResult.Errors);

			if (result.Errors.Any())
			{
				return result;
			}

			if (validDateToResult.Data.Value < validDateFromResult.Data.Value)
			{
				var errorMessage = string.Format(Validations.GreaterThanOrEqualTo_TwoFields, DataDictionary.ValidDateTo, DataDictionary.ValidDateFrom);

				result.AddError(errorMessage);
			}

            if (result.Errors.Any())
            {
                return result;
            }

            var returnValue = new DiscountCoupon(discountPercentResult.Data,
				validDateFromResult.Data, validDateToResult.Data);

			result.SetData(returnValue);

			return result;
		}

		private DiscountCoupon()
        {
		}

		private DiscountCoupon(DiscountPercent discountPercent, ValidDateFrom validDateFrom, ValidDateTo validDateTo) : this()
		{
			ValidDateTo = validDateTo;
			ValidDateFrom = validDateFrom;
			DiscountPercent = discountPercent;
		}

		public ValidDateTo ValidDateTo { get; private set; } = ValidDateTo.Default;

		public ValidDateFrom ValidDateFrom { get; private set; } = ValidDateFrom.Default;

		public DiscountPercent DiscountPercent { get; private set; } = DiscountPercent.Default;

	
		public FluentResult Update
			(int? discountPercent, DateTime? validDateFrom, DateTime? validDateTo)
        {
            var result = Create(discountPercent, validDateFrom, validDateTo);

            if (result.Errors.Any())
			{
				return result;
			}

			ValidDateTo = result.Data.ValidDateTo;
			ValidDateFrom = result.Data.ValidDateFrom;
			DiscountPercent = result.Data.DiscountPercent;

			return result;
		}
	}
}

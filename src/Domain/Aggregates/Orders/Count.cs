namespace Domain.Aggregates.Orders
{
	public class Count : SeedWork.ValueObject
	{
		#region Constant(s)
		public const int Minimum = 1;

		public const int Maximum = 10;
		#endregion /Constant(s)

		#region Static Member(s)
		public static FluentResults.Result<Count> Create(int value)
		{
			var result =
				new FluentResults.Result<Count>();

			if ((value < Minimum) || (value > Maximum))
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Range, Resources.DataDictionary.Count, Minimum, Maximum);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var returnValue =
				new Count(value: value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		/// <summary>
		/// For EF Core!
		/// </summary>
		private Count() : base()
		{
		}

		protected Count(int value) : this()
		{
			Value = value;
		}

		public int Value { get; private set; }

		protected override
			System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
		{
			yield return Value;
		}

		public override string ToString()
		{
			return Value.ToString();
		}
	}
}

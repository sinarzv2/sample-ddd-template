namespace Domain.Aggregates.Orders
{
	public class Payment : SeedWork.Entity
	{
		/// <summary>
		/// For EF Core!
		/// </summary>
		private Payment() : base()
		{
		}

		public Payment(SharedKernel.Money amount) : this()
		{
			Amount = amount;
		}

		public SharedKernel.Money Amount { get; private set; }
	}
}

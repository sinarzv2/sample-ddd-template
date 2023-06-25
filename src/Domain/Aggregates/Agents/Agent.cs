using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Agents
{
    public class Agent : SeedWork.AggregateRoot
	{
		/// <summary>
		/// For EF Core!
		/// </summary>
		private Agent() : base()
		{
		}

		public Agent(FullName fullName) : this()
		{
			if (fullName is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(fullName));
			}

			FullName = fullName;
		}

		public FullName FullName { get; private set; }
	}
}

namespace Domain.Aggregates.Packages
{
	public class Feature : SeedWork.AggregateRoot
	{

		private Feature() : base()
		{
		}

		public Feature(ValueObjects.FeatureName name, ValueObjects.PackageName packageName, Feature parent)
		{
			if (name is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(name));
			}

			if (packageName is null)
			{
				throw new System.ArgumentNullException(paramName: nameof(packageName));
			}

			// Parent can be null!

			Name = name;
			Parent = parent;
			PackageName = packageName;
		}

		public Feature Parent { get; private set; }

		public ValueObjects.FeatureName Name { get; private set; }

		public ValueObjects.PackageName PackageName { get; private set; }
	}
}

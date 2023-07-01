using Domain.Aggregates.Features.ValueObjects;
using Domain.SeedWork;

namespace Domain.Aggregates.Features
{
	public class Feature : AggregateRoot
	{

		private Feature()
        {
		}

		public Feature(FeatureName name, PackageName packageName, Feature parent)
		{

			Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
			Parent = parent;
			PackageName = packageName ?? throw new ArgumentNullException(paramName: nameof(packageName));
		}

		public Feature Parent { get; }

		public FeatureName Name { get; } = FeatureName.Default;

		public PackageName PackageName { get; } = PackageName.Default;
	}
}

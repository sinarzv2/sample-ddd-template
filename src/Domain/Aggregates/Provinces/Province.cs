using System.Linq;

namespace Domain.Aggregates.Provinces
{
	public class Province : SeedWork.AggregateRoot
	{
		#region Static Member(s)
		public static FluentResults.Result<Province> Create(string name)
		{
			var result =
				new FluentResults.Result<Province>();

			// **************************************************
			var nameResult =
				SharedKernel.Name.Create(value: name);

			result.WithErrors(errors: nameResult.Errors);
			// **************************************************

			if (result.IsFailed)
			{
				return result;
			}

			var returnValue =
				new Province(name: nameResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private Province() : base()
		{
			_cities =
				new System.Collections.Generic.List<Cities.City>();
		}

		private Province(SharedKernel.Name name) : this()
		{
			Name = name;
		}

		public SharedKernel.Name Name { get; private set; }

		// **********
		private readonly System.Collections.Generic.List<Cities.City> _cities;

		public virtual System.Collections.Generic.IReadOnlyList<Cities.City> Cities
		{
			get
			{
				return _cities;
			}
		}
		// **********

		public FluentResults.Result Update(string name)
		{
			var result =
				Create(name: name);

			if (result.IsFailed)
			{
				return result.ToResult();
			}

			Name = result.Value.Name;

			return result.ToResult();
		}

		public FluentResults.Result<Cities.City> AddCity(string cityName)
		{
			var result =
				new FluentResults.Result<Cities.City>();

			// **************************************************
			var cityResult =
				Aggregates.Cities.City.Create(province: this, name: cityName);

			if (cityResult.IsFailed)
			{
				result.WithErrors(errors: cityResult.Errors);

				return result;
			}
			// **************************************************

			// **************************************************
			var hasAny =
				_cities
				.Where(current => current.Name.Value.ToLower()
					== cityResult.Value.Name.Value.ToLower())
				.Any();

			if (hasAny)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Repetitive,
					Resources.DataDictionary.CityName);

				result.WithError(errorMessage: errorMessage);

				return result;
			}
			// **************************************************

			_cities.Add(cityResult.Value);

			result.WithValue(cityResult.Value);

			return result;
		}

		public FluentResults.Result RemoveCity(string cityName)
		{
			var result =
				new FluentResults.Result();

			// **************************************************
			var cityResult =
				Aggregates.Cities.City.Create(province: this, name: cityName);

			if (cityResult.IsFailed)
			{
				result.WithErrors(errors: cityResult.Errors);

				return result;
			}
			// **************************************************

			// **************************************************
			var foundedCity =
				_cities
				.Where(current => current.Name.Value.ToLower() == cityResult.Value.Name.Value.ToLower())
				.FirstOrDefault();

			if (foundedCity == null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.NotFound, Resources.DataDictionary.City);

				result.WithError(errorMessage: errorMessage);

				return result;
			}
			// **************************************************

			_cities.Remove(foundedCity);

			return result;
		}
	}
}

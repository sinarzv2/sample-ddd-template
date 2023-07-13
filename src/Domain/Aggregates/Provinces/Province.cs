using Common.Models;
using Common.Resources;
using Common.Resources.Messages;
using Domain.Aggregates.Cities;
using Domain.SeedWork;
using Domain.SharedKernel.ValueObjects;

namespace Domain.Aggregates.Provinces
{
    public class Province : AggregateRoot
    {
        public static FluentResult<Province> Create(string name, Guid? id = null)
        {
            var result = new FluentResult<Province>();

            var nameResult = Name.Create(name);

            result.AddErrors(nameResult.Errors);

            if (result.Errors.Any())
            {
                return result;
            }

            var returnValue = new Province(nameResult.Data, id);

            result.SetData(returnValue);

            return result;
        }

        private Province()
        {
            _cities = new List<City>();
        }

        private Province(Name name, Guid? id = null) : this()
        {
            if (id != null)
                Id = id.Value;
            Name = name;
        }

        public Name Name { get; private set; } = Name.Default;

        private readonly List<City> _cities;

        public virtual IReadOnlyList<City> Cities => _cities;

        public FluentResult Update(string name)
        {
            var result = Create(name);

            if (result.Errors.Any())
            {
                return result;
            }

            Name = result.Data.Name;

            return result;
        }

        public FluentResult<City> AddCity(string cityName)
        {
            var result = new FluentResult<City>();

            var cityResult = City.Create(this, cityName);

            if (cityResult.Errors.Any())
            {
                result.AddErrors(cityResult.Errors);

                return result;
            }

            var hasAny = _cities.Any(current => current.Name.Value.ToLower() == cityResult.Data.Name.Value.ToLower());

            if (hasAny)
            {
                var errorMessage = string.Format(Validations.Repetitive, DataDictionary.CityName);

                result.AddError(errorMessage);

                return result;
            }

            _cities.Add(cityResult.Data);

            result.SetData(cityResult.Data);

            return result;
        }

        public FluentResult RemoveCity(string cityName)
        {
            var result = new FluentResult();


            var cityResult = City.Create(this, cityName);

            if (cityResult.Errors.Any())
            {
                result.AddErrors(cityResult.Errors);

                return result;
            }

            var foundedCity = _cities.FirstOrDefault(current => current.Name.Value.ToLower() == cityResult.Data.Name.Value.ToLower());

            if (foundedCity == null)
            {
                var errorMessage = string.Format(Validations.NotFound, DataDictionary.City);

                result.AddError(errorMessage);

                return result;
            }

            _cities.Remove(foundedCity);

            return result;
        }
    }
}

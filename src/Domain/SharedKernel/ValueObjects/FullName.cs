using Common.Models;
using Domain.SeedWork;
using Domain.SharedKernel.Enumerations;

namespace Domain.SharedKernel.ValueObjects
{
    public class FullName : ValueObject
    {
        public static FluentResult<FullName> Create(int? gender, string firstName, string lastName)
        {
            var result = new FluentResult<FullName>();

            var genderResult = Gender.GetByValue(gender);

            result.AddErrors(genderResult.Errors);

            var firstNameResult = FirstName.Create(firstName);

            result.AddErrors(firstNameResult.Errors);

            var lastNameResult = LastName.Create(lastName);

            result.AddErrors(lastNameResult.Errors);
            

            if (result.Errors.Any())
            {
                return result;
            }

            var returnValue = new FullName(genderResult.Data, firstNameResult.Data, lastNameResult.Data);

            result.SetData(returnValue);

            return result;
        }
        

        private FullName()
        {
        }

        private FullName(Gender gender, FirstName firstName, LastName lastName) : this()
        {
            Gender = gender;
            LastName = lastName;
            FirstName = firstName;
        }

        public LastName? LastName { get; } 

        public FirstName? FirstName { get; }

        public virtual Gender? Gender { get; }

        protected override IEnumerable<object?> GetEqualityComponents()
        {
            yield return Gender;
            yield return FirstName;
            yield return LastName;
        }

        public override string ToString()
        {
            var result = $"{Gender?.Name} {FirstName?.Value} {LastName?.Value}".Trim();

            return result;
        }
    }
}

namespace Domain.SharedKernel
{
	public class FullName : SeedWork.ValueObject
	{
		#region Static Member(s)
		public static FluentResults.Result<FullName>
			Create(int? gender, string firstName, string lastName)
		{
			var result =
				new FluentResults.Result<FullName>();

			// **************************************************
			var genderResult =
				Gender.GetByValue(value: gender);

			result.WithErrors(genderResult.Errors);
			// **************************************************

			// **************************************************
			var firstNameResult =
				FirstName.Create(value: firstName);

			result.WithErrors(firstNameResult.Errors);
			// **************************************************

			// **************************************************
			var lastNameResult =
				LastName.Create(value: lastName);

			result.WithErrors(lastNameResult.Errors);
			// **************************************************

			if (result.IsFailed)
			{
				return result;
			}

			var returnValue =
				new FullName(gender: genderResult.Value,
				firstName: firstNameResult.Value, lastName: lastNameResult.Value);

			result.WithValue(value: returnValue);

			return result;
		}
		#endregion /Static Member(s)

		private FullName() : base()
		{
		}

		private FullName
			(Gender gender, FirstName firstName, LastName lastName) : this()
		{
			Gender = gender;
			LastName = lastName;
			FirstName = firstName;
		}

		public LastName LastName { get; }

		public FirstName FirstName { get; }

		public virtual Gender Gender { get; }

		protected override System.Collections.Generic.IEnumerable<object> GetEqualityComponents()
		{
			yield return Gender;
			yield return FirstName;
			yield return LastName;
		}

		public override string ToString()
		{
			string result =
				$"{ Gender?.Name } { FirstName?.Value } { LastName?.Value }".Trim();

			return result;
		}
	}
}

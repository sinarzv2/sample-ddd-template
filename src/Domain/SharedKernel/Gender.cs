namespace Domain.SharedKernel
{
	public class Gender : SeedWork.Enumeration
	{
		#region Constant(s)
		public const int MaxLength = 10;
		#endregion /Constant(s)

		#region Static Member(s)
		public static readonly Gender Male = new(0, Resources.DataDictionary.Male);
		public static readonly Gender Female = new(1, Resources.DataDictionary.Female);

		public static FluentResults.Result<Gender> GetByValue(int? value)
		{
			var result =
				new FluentResults.Result<Gender>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.Gender);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var gender =
				FromValue<Gender>(value: value.Value);

			if (gender is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.Gender);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			result.WithValue(gender);

			return result;
		}
		#endregion /Static Member(s)

		private Gender() : base()
		{
		}

		private Gender(int value, string name) : base(value: value, name: name)
		{
		}
	}
}

//	public class Gender : SeedWork.Enumeration
//	{
//		#region Static Member(s)
//		public static readonly Gender Male = new MaleType();
//		public static readonly Gender Female = new FemaleType();

//		public static FluentResults.Result<Gender> GetByValue(int? value)
//		{
//			var result =
//				new FluentResults.Result<Gender>();

//			if (value is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.Required, Resources.DataDictionary.Gender);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}

//			var gender =
//				FromValue<Gender>(value: value.Value);

//			if (gender is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.Gender);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}

//			result.WithValue(gender);

//			return result;
//		}
//		#endregion /Static Member(s)

//		private Gender() : base()
//		{
//		}

//		private Gender(int value, string name) : base(value: value, name: name)
//		{
//		}

//		#region Nested Class(es)
//		private class MaleType : Gender
//		{
//			public MaleType() : base(value: 1, name: Resources.DataDictionary.Male)
//			{
//			}
//		}

//		private class FemaleType : Gender
//		{
//			public FemaleType() : base(value: 2, name: Resources.DataDictionary.Female)
//			{
//			}
//		}
//		#endregion /Nested Class(es)
//	}
//}

//namespace Domain.SharedKernel
//{
//	public class Gender : SeedWork.Enumeration
//	{
//		// Solution (1)
//		//public static readonly Gender Male = new Gender(0, "Male");
//		//public static readonly Gender Female = new Gender(1, "Female");
//		// /Solution (1)

//		// Solution (2)
//		//public static readonly Gender Male = new Gender(0, nameof(Male));
//		//public static readonly Gender Female = new Gender(1, nameof(Female));
//		// /Solution (2)

//		// Solution (3)
//		//public static readonly Gender Male = new(0, nameof(Male));
//		//public static readonly Gender Female = new(1, nameof(Female));
//		// /Solution (3)

//		// Solution (4)
//		public static readonly Gender Male = new(0, Resources.DataDictionary.Male);
//		public static readonly Gender Female = new(1, Resources.DataDictionary.Female);
//		// /Solution (4)

//		/// <summary>
//		/// For EF Core!
//		/// </summary>
//		private Gender() : base()
//		{
//		}

//		protected Gender(int value, string name) : base(value: value, name: name)
//		{
//		}
//	}
//}

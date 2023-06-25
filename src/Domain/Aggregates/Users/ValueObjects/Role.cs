namespace Domain.Aggregates.Users.ValueObjects
{
	public class Role : SeedWork.Enumeration
	{
		#region Constant(s)
		public const int MaxLength = 50;
		#endregion /Constant(s)

		#region Static Member(s)
		public static readonly Role Customer = new(value: 0, name: Resources.DataDictionary.Customer);
		public static readonly Role Agent = new(value: 1, name: Resources.DataDictionary.Agent);
		public static readonly Role Supervisor = new(value: 2, name: Resources.DataDictionary.Supervisor);
		public static readonly Role Administrator = new(value: 3, name: Resources.DataDictionary.Administrator);
		public static readonly Role Programmer = new(value: 4, name: Resources.DataDictionary.Programmer);

		public static FluentResults.Result<Role> GetByValue(int? value)
		{
			var result =
				new FluentResults.Result<Role>();

			if (value is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.Required, Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			var role =
				FromValue<Role>(value: value.Value);

			if (role is null)
			{
				string errorMessage = string.Format
					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.UserRole);

				result.WithError(errorMessage: errorMessage);

				return result;
			}

			result.WithValue(role);

			return result;
		}
		#endregion /Static Member(s)

		private Role() : base()
		{
		}

		private Role(int value, string name) : base(value: value, name: name)
		{
		}
	}
}

//namespace Domain.Aggregates.Users.ValueObjects
//{
//	public class Role : SeedWork.Enumeration
//	{
//		#region Static Member(s)
//		public static readonly Role Agent = new AgentType();
//		public static readonly Role Customer = new CustomerType();
//		public static readonly Role Programmer = new ProgrammerType();
//		public static readonly Role Supervisor = new SupervisorType();
//		public static readonly Role Administrator = new AdministratorType();

//		public static FluentResults.Result<Role> GetByValue(int? value)
//		{
//			var result =
//				new FluentResults.Result<Role>();

//			if (value is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.Required, Resources.DataDictionary.UserRole);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}

//			var role =
//				SeedWork.Enumeration.FromValue<Role>(value: value.Value);

//			if (role is null)
//			{
//				string errorMessage = string.Format
//					(Resources.Messages.Validations.InvalidCode, Resources.DataDictionary.UserRole);

//				result.WithError(errorMessage: errorMessage);

//				return result;
//			}

//			result.WithValue(role);

//			return result;
//		}
//		#endregion /Static Member(s)

//		private Role() : base()
//		{
//		}

//		private Role(int value, string name) : base(value: value, name: name)
//		{
//		}

//		#region Nested Class(es)
//		private class CustomerType : Role
//		{
//			public CustomerType() : base(value: 0, name: Resources.DataDictionary.Customer)
//			{
//			}
//		}

//		private class AgentType : Role
//		{
//			public AgentType() : base(value: 1, name: Resources.DataDictionary.Agent)
//			{
//			}
//		}

//		private class SupervisorType : Role
//		{
//			public SupervisorType() : base(value: 2, name: Resources.DataDictionary.Supervisor)
//			{
//			}
//		}

//		private class AdministratorType : Role
//		{
//			public AdministratorType() : base(value: 3, name: Resources.DataDictionary.Administrator)
//			{
//			}
//		}

//		/// <summary>
//		/// TODO: public -> private
//		/// </summary>
//		public class ProgrammerType : Role
//		{
//			public ProgrammerType() : base(value: 4, name: Resources.DataDictionary.Programmer)
//			{
//			}
//		}
//		#endregion /Nested Class(es)
//	}
//}

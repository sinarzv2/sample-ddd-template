//using Common.Models;
//using Domain.SeedWork;
//using Domain.SharedKernel.ValueObjects;
//using Microsoft.AspNetCore.Identity;

//namespace Domain.Aggregates.Users
//{
//	public class User : IdentityUser
//	{
//		public static FluentResult<User> Create
//			(string username, string password, string emailAddress,
//			string cellPhoneNumber, int? role, int? gender, string firstName, string lastName)
//		{
//			var result = new FluentResult<User>();

//			var usernameResult = Username.Create(value: username);

//			result.WithErrors(errors: usernameResult.Errors);
//			// **************************************************

//			// **************************************************
//			var passwordResult =
//				ValueObjects.Password.Create(value: password);

//			result.WithErrors(errors: passwordResult.Errors);
//			// **************************************************

//			// **************************************************
//			var emailAddressResult =
//				EmailAddress.Create(value: emailAddress);

//			result.WithErrors(errors: emailAddressResult.Errors);
//			// **************************************************

//			// **************************************************
//			var cellPhoneNumberResult =
//				CellPhoneNumber.Create(value: cellPhoneNumber);

//			result.WithErrors(errors: cellPhoneNumberResult.Errors);
//			// **************************************************

//			// **************************************************
//			var roleResult =
//				ValueObjects.Role.GetByValue(value: role);

//			result.WithErrors(errors: roleResult.Errors);
//			// **************************************************

//			// **************************************************
//			var fullNameResult =
//				FullName.Create
//				(gender: gender, firstName: firstName, lastName: lastName);

//			result.WithErrors(errors: fullNameResult.Errors);
//			// **************************************************

//			if (result.IsFailed)
//			{
//				return result;
//			}

//			var returnValue =
//				new User(username: usernameResult.Value, password: passwordResult.Value,
//				emailAddress: emailAddressResult.Value, cellPhoneNumber: cellPhoneNumberResult.Value,
//				role: roleResult.Value, fullName: fullNameResult.Value);

//			result.WithValue(value: returnValue);

//			return result;
//		}
//#endregion /Static Member(s)

//		private User() : base()
//		{
//		}

//		private User
//			(ValueObjects.Username username,
//			ValueObjects.Password password,
//			EmailAddress emailAddress,
//			CellPhoneNumber cellPhoneNumber,
//			ValueObjects.Role role,
//			FullName fullName) : this()
//		{
//			Role = role;
//			FullName = fullName;
//			Username = username;
//			Password = password;
//			EmailAddress = emailAddress;
//			CellPhoneNumber = cellPhoneNumber;
//		}

//		public ValueObjects.Role Role { get; private set; }

//		public ValueObjects.Username Username { get; private set; }

//		public ValueObjects.Password Password { get; private set; }

//		public FullName FullName { get; private set; }

//		public EmailAddress EmailAddress { get; private set; }

//		public CellPhoneNumber CellPhoneNumber { get; private set; }

//		/// <summary>
//		/// TODO: Check Old Password!
//		/// </summary>
//		/// <param name="newPassword"></param>
//		/// <returns></returns>
//		public FluentResults.Result ChangePassword(string newPassword)
//		{
//			var result =
//				new FluentResults.Result();

//			var newPasswordResult =
//				ValueObjects.Password.Create(value: newPassword);

//			if (newPasswordResult.IsFailed)
//			{
//				result.WithErrors(errors: newPasswordResult.Errors);

//				return result;
//			}

//			Password =
//				newPasswordResult.Value;

//			RaiseDomainEvent
//				(new Events.UserPasswordChangedEvent
//				(fullName: FullName, emailAddress: EmailAddress));

//			return result;
//		}
//	}
//}

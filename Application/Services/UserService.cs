using System.Linq;

namespace Application.Services
{
	public class UserService : object, IUserService
	{
		public UserService
			(Microsoft.Extensions.Options.IOptions
			<Infrastructure.ApplicationSettings.Main> options) : base()
		{
			MainSettings = options.Value;
		}

		protected Infrastructure.ApplicationSettings.Main MainSettings { get; }

		// **************************************************
		// Test (Mock) Data!
		// **************************************************
		private System.Collections.Generic.List<Models.User> _users;

		/// <summary>
		/// Lazy Loading = Lazy Initialization
		/// </summary>
		protected System.Collections.Generic.List<Models.User> Users
		{
			get
			{
				if (_users == null)
				{
					_users =
						new System.Collections.Generic.List<Models.User>();

					for (int index = 1; index < 5; index++)
					{
						Models.User user =
							new Models.User
							{
								Id = index,
								Password = "1234567890",
								Username = $"Username{ index }",
								LastName = $"Last Name { index }",
								FirstName = $"First Name { index }",
								EmailAddress = $"Email{ index }.@Gmail.com",
							};

						_users.Add(user);
					}
				}

				return _users;
			}
		}
		// **************************************************

		public Models.User GetById(int id)
		{
			Models.User foundedUser =
				Users
				.Where(current => current.Id == id)
				.FirstOrDefault();

			return foundedUser;
		}

		public System.Collections.Generic.IEnumerable<Models.User> GetAll()
		{
			return Users;
		}

		public ViewModels.Users.LoginResponseViewModel
			Login(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			if (viewModel == null)
			{
				return null;
			}

			if (string.IsNullOrWhiteSpace(viewModel.Username))
			{
				return null;
			}

			if (string.IsNullOrWhiteSpace(viewModel.Password))
			{
				return null;
			}

			Models.User foundedUser =
				Users
				.Where(current => current.Username.ToLower() == viewModel.Username.ToLower())
				.FirstOrDefault();

			if (foundedUser == null)
			{
				return null;
			}

			// Note: Password should be in HASH!
			if (string.Compare(foundedUser.Password, viewModel.Password, ignoreCase: false) != 0)
			{
				return null;
			}

			// Authentication successful so generate jwt token

			string token =
				Infrastructure.JwtUtility.GenerateJwtToken
				(user: foundedUser, mainSettings: MainSettings);

			ViewModels.Users.LoginResponseViewModel response =
				new ViewModels.Users.LoginResponseViewModel(user: foundedUser, token: token);

			return response;
		}
	}
}

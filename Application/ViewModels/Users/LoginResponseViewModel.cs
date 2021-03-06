﻿//namespace Application.ViewModels.Users

namespace ViewModels.Users
{
	public class LoginResponseViewModel : object
	{
		//public LoginResponseViewModel() : base()
		//{
		//}

		public LoginResponseViewModel(Models.User user, string token) : base()
		{
			if (user == null)
			{
				throw new System.ArgumentNullException(paramName: nameof(user));
			}

			if (string.IsNullOrWhiteSpace(token))
			{
				throw new System.ArgumentNullException(paramName: nameof(token));
			}

			Token = token;

			Id = user.Id;
			Username = user.Username;
			LastName = user.LastName;
			FirstName = user.FirstName;
		}

		public int Id { get; set; }

		public string Token { get; set; }

		public string Username { get; set; }

		public string LastName { get; set; }

		public string FirstName { get; set; }
	}
}

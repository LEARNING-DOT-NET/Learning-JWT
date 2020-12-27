//namespace Application.ViewModels.Users

namespace ViewModels.Users
{
	public class LoginResponseViewModel : object
	{
		//public LoginResponseViewModel()
		//{
		//}

		public LoginResponseViewModel(Models.User user, string token)
		{
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

//namespace Application.ViewModels.Users

namespace ViewModels.Users
{
	public class LoginRequestViewModel : object
	{
		public LoginRequestViewModel() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.Required]
		public string Username { get; set; }

		[System.ComponentModel.DataAnnotations.Required]
		public string Password { get; set; }
	}
}

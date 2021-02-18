//namespace Application.ViewModels.Users

namespace ViewModels.Users
{
	public class LoginRequestViewModel : object
	{
		public LoginRequestViewModel() : base()
		{
		}

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]
		public string Username { get; set; }

		[System.ComponentModel.DataAnnotations.Required
			(AllowEmptyStrings = false)]
		public string Password { get; set; }
	}
}

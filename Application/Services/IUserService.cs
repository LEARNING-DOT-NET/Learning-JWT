namespace Application.Services
{
	public interface IUserService
	{
		Models.User GetById(int id);

		System.Collections.Generic.IEnumerable<Models.User> GetAll();

		ViewModels.Users.LoginResponseViewModel
			Login(ViewModels.Users.LoginRequestViewModel viewModel);
	}
}

namespace Application.Controllers
{
	[Microsoft.AspNetCore.Mvc.ApiController]
	[Microsoft.AspNetCore.Mvc.Route("[controller]")]
	public class UsersController : Microsoft.AspNetCore.Mvc.ControllerBase
	{
		//public UsersController() : base()
		//{
		//}

		public UsersController(Services.IUserService userService)
		{
			UserService = userService;
		}

		protected Services.IUserService UserService { get; }

		//[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		//public ViewModels.Users.LoginResponseViewModel
		//	Login(ViewModels.Users.LoginRequestViewModel viewModel)
		//{
		//	ViewModels.Users.LoginResponseViewModel
		//		response = UserService.Login(viewModel);

		//	if (response == null)
		//	{
		//		return null;
		//	}

		//	return response;
		//}

		//[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		//public ViewModels.Users.LoginResponseViewModel
		//	Login(ViewModels.Users.LoginRequestViewModel viewModel)
		//{
		//	ViewModels.Users.LoginResponseViewModel
		//		response = UserService.Login(viewModel);

		//	return response;
		//}

		//[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		//public Microsoft.AspNetCore.Mvc.IActionResult
		//	Login(ViewModels.Users.LoginRequestViewModel viewModel)
		//{
		//	ViewModels.Users.LoginResponseViewModel
		//		response = UserService.Login(viewModel);

		//	if (response == null)
		//	{
		//		return BadRequest
		//			(new { message = "Username and/or password is not correct!" });
		//	}

		//	return Ok(response);
		//}

		//[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]
		//public Microsoft.AspNetCore.Mvc.ActionResult<ViewModels.Users.LoginResponseViewModel>
		//	Login(ViewModels.Users.LoginRequestViewModel viewModel)
		//{
		//	ViewModels.Users.LoginResponseViewModel
		//		response = UserService.Login(viewModel);

		//	if (response == null)
		//	{
		//		return BadRequest
		//			(new { message = "Username and/or password is not correct!" });
		//	}

		//	return Ok(response);
		//}

		#region Login
		// **************************************************
		[Microsoft.AspNetCore.Mvc.HttpPost(template: "login")]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(typeof(ViewModels.Users.LoginResponseViewModel),
			Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]

		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(typeof(ViewModels.General.ErrorViewModel),
			Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest)]
		// **************************************************
		public
			async
			System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult<ViewModels.Users.LoginResponseViewModel>>
			Login
			(ViewModels.Users.LoginRequestViewModel viewModel)
		{
			ViewModels.Users.LoginResponseViewModel response = null;

			//response =
			//	UserService.Login(viewModel);

			//response =
			//	await UserService.Login(viewModel);

			await System.Threading.Tasks.Task.Run(() =>
			{
				response =
					UserService.Login(viewModel);
			});

			if (response == null)
			{
				string errorMessage =
					"Username and/or password is not correct!";

				return BadRequest
					(new ViewModels.General.ErrorViewModel(message: errorMessage));
			}

			return Ok(response);
		}
		#endregion /Login

		[Microsoft.AspNetCore.Mvc.HttpGet(template: "GetAll")]
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IEnumerable<Models.User>>>
			GetAll()
		{
			//var response =
			//	UserService.GetAll();

			System.Collections.Generic.IEnumerable<Models.User> response = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				response =
					UserService.GetAll();
			});

			return Ok(response);
		}

		[Microsoft.AspNetCore.Mvc.HttpGet]
		[Infrastructure.Attributes.Authorize]
		[Microsoft.AspNetCore.Mvc.ProducesResponseType
			(Microsoft.AspNetCore.Http.StatusCodes.Status200OK)]
		public async System.Threading.Tasks.Task
			<Microsoft.AspNetCore.Mvc.ActionResult<System.Collections.Generic.IEnumerable<Models.User>>>
			Get()
		{
			System.Collections.Generic.IEnumerable<Models.User> response = null;

			await System.Threading.Tasks.Task.Run(() =>
			{
				response =
					UserService.GetAll();
			});

			return Ok(response);
		}
	}
}

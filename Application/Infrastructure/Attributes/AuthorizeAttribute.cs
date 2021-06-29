//namespace Application.Infrastructure.Attributes

namespace Infrastructure.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
	public class AuthorizeAttribute :
		System.Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
	{
		public AuthorizeAttribute() : base()
		{
		}

		//public void OnAuthorization
		//	(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
		//{
		//	throw new System.NotImplementedException();
		//}

		public void OnAuthorization
			(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
		{
			// احمقانه‌ترین روش کست کردن است
			//Models.User user =
			//	(Models.User)context.HttpContext.Items["User"];

			var user =
				context.HttpContext.Items["User"] as Models.User;

			// Not Logged in or Request with Crupted Token or Request with Expired Token!
			if (user == null)
			{
				context.Result =
					new Microsoft.AspNetCore.Mvc
					.JsonResult(new { message = "Unauthorized" })
					{
						StatusCode =
							Microsoft.AspNetCore.Http.StatusCodes.Status401Unauthorized,
					};
			}

			// If User != null

			//context.HttpContext.Request.Path
			//	/Products/Create

			// Request to Database!
		}
	}

	//public class UserLogFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
	//{
	//	public void OnActionExecuting(ActionExecutingContext context)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	public void OnActionExecuted(ActionExecutedContext context)
	//	{
	//		throw new System.NotImplementedException();
	//	}
	//}

	//public class UserScoreFilterAttribute : Microsoft.AspNetCore.Mvc.Filters.IActionFilter
	//{
	//	public void OnActionExecuting(ActionExecutingContext context)
	//	{
	//		throw new System.NotImplementedException();
	//	}

	//	public void OnActionExecuted(ActionExecutedContext context)
	//	{
	//		throw new System.NotImplementedException();
	//	}
	//}
}

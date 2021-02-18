//namespace Application.Infrastructure.Attributes

using Microsoft.AspNetCore.Mvc.Filters;

namespace Infrastructure.Attributes
{
	[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
	public class AuthorizeAttribute :
		System.Attribute, Microsoft.AspNetCore.Mvc.Filters.IAuthorizationFilter
	{
		public AuthorizeAttribute() : base()
		{
		}

		public void OnAuthorization
			(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
		{
			//Models.User user =
			//	(Models.User)context.HttpContext.Items["User"];

			Models.User user =
				context.HttpContext.Items["User"] as Models.User;

			//context.HttpContext.Request.Path

			// Not Logged in
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

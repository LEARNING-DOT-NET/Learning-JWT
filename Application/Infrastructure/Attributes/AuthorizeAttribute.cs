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

		public void OnAuthorization
			(Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext context)
		{
			Models.User user =
				context.HttpContext.Items["User"] as Models.User;

			//context.HttpContext.Request.Path

			if (user == null)
			{
				// Not Logged in
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
}

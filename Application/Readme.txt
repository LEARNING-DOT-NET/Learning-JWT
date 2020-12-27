--------------------------------------------------
https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0
--------------------------------------------------

--------------------------------------------------
Install Packages:

Install-Package System.IdentityModel.Tokens.Jwt

	Microsoft.IdentityModel.Tokens (>= 6.8.0)

		Microsoft.IdentityModel.Logging (>= 6.8.0)

	Microsoft.IdentityModel.JsonWebTokens (>= 6.8.0)

		Microsoft.IdentityModel.Tokens (>= 6.8.0)

Install-Package Microsoft.AspNetCore.Authentication.JwtBearer

	Microsoft.IdentityModel.Protocols.OpenIdConnect (>= 6.7.1)

		System.IdentityModel.Tokens.Jwt (>= 6.8.0)

		Microsoft.IdentityModel.Protocols (>= 6.8.0)

			Microsoft.IdentityModel.Tokens (>= 6.8.0)

با توجه به موارد فوق نصب صرفا
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
کافی است
--------------------------------------------------

--------------------------------------------------
Update appsettings.json File:

{
	"Logging": {
		"LogLevel": {
			"Default": "Information",
			"Microsoft": "Warning",
			"Microsoft.Hosting.Lifetime": "Information"
		}
	},
	"AllowedHosts": "*",

	"AppSettings": {
		"SecretKey": "asfdadsfsafdsfdsafdsafdsafdsafdsaf"

	}
}
--------------------------------------------------

--------------------------------------------------
Create Folder: Models

Create Class (File) in Models Folder: User.cs

Note:
[System.Text.Json.Serialization.JsonIgnore]
--------------------------------------------------

--------------------------------------------------
Create Folder: Infrastructure
Create Folder in Infrastructure Folder: Attributes

Create Class (File) in Attributes Folder: AuthorizeAttribute.cs

Note:
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
--------------------------------------------------

--------------------------------------------------
Create Folder in Infrastructure Folder: ApplicationSettings

Create Class (File) in ApplicationSettings Folder: Main.cs
--------------------------------------------------

--------------------------------------------------
Create Folder: ViewModels
Create Folder in ViewModels Folder: Users

Create Class (File) in Users Folder: LoginRequestViewModel.cs
Create Class (File) in Users Folder: LoginResponseViewModel.cs

Note:
LoginResponseViewModel Class does not have Default Constructor!
--------------------------------------------------

--------------------------------------------------
Create Class (File) in Infrastructure Folder: JwtUtility.cs
--------------------------------------------------

--------------------------------------------------
Create Folder: Services

Create Interface (File) in Services Folder: IUserService.cs
Create Class (File) in Services Folder: UserService.cs
--------------------------------------------------

--------------------------------------------------
Create Folder in Infrastructure Folder: Middlewares
Create Class (File) in Middlewares Folder: JwtMiddleware.cs
--------------------------------------------------

--------------------------------------------------
Create Class (File) in Controllers Folder: UsersController.cs

Note:
[Infrastructure.Attributes.Authorize]
--------------------------------------------------

--------------------------------------------------
Make Changes in Startup.cs File:

	Function:

		services.AddCors();
		services.AddControllers();

		// Configure strongly typed settings object
		services.Configure<AppSettings>(Configuration.GetSection("AppSettings"));

		// Configure DI for application services
		services.AddScoped<IUserService, UserService>();
--------------------------------------------------

--------------------------------------------------
Update Startup.cs File:

		public void ConfigureServices
			(Microsoft.Extensions.DependencyInjection.IServiceCollection services)
		{
			services.AddCors();
			services.AddControllers();

			// Configure strongly typed settings object
			services.Configure<Infrastructure.ApplicationSettings.Main>
				(Configuration.GetSection("AppSettings"));

			// Configure DI for application services
			services.AddScoped<Services.IUserService, Services.UserService>();
		}

		public void Configure
			(Microsoft.AspNetCore.Builder.IApplicationBuilder app,
			Microsoft.AspNetCore.Hosting.IWebHostEnvironment env)
		{
			app.UseHttpsRedirection();

			app.UseRouting();

			// Global cors policy
			app.UseCors(current => current
				.AllowAnyOrigin()
				.AllowAnyMethod()
				.AllowAnyHeader());

			// Custom JWT auth middleware
			app.UseMiddleware<Infrastructure.Middlewares.JwtMiddleware>();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
--------------------------------------------------

--------------------------------------------------
Tests:

	Login:

		(1)
		https://localhost:44390/users/login

		(2)
		https://localhost:44390/users/login

		{"username": "Username1", "password": "1234567890"}
		{"username": "Username1", "password": "temp"}
		{"username": "temp", "password": "1234567890"}
--------------------------------------------------

--------------------------------------------------
(1)
https://jasonwatmore.com/post/2019/10/11/aspnet-core-3-jwt-authentication-tutorial-with-example-api
https://docs.microsoft.com/en-us/aspnet/core/web-api/action-return-types?view=aspnetcore-5.0
--------------------------------------------------

--------------------------------------------------
(2)
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
(3)
Update appsettings.json File (Add AppSettings Key):

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
(4)
Create Folder: Models

Create Class (File) in Models Folder: User.cs

Note:
[System.Text.Json.Serialization.JsonIgnore]
--------------------------------------------------

--------------------------------------------------
(5)
Create Folder: Infrastructure
Create Folder in Infrastructure Folder: Attributes

Create Class (File) in Attributes Folder: AuthorizeAttribute.cs

Note:
[System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Method)]
--------------------------------------------------

--------------------------------------------------
(6)
Create Folder in Infrastructure Folder: ApplicationSettings

Create Class (File) in ApplicationSettings Folder: Main.cs
--------------------------------------------------

--------------------------------------------------
(7)
Create Folder: ViewModels
Create Folder in ViewModels Folder: Users

Create Class (File) in Users Folder: LoginRequestViewModel.cs
Create Class (File) in Users Folder: LoginResponseViewModel.cs

Note:
LoginResponseViewModel Class does not have Default Constructor!
--------------------------------------------------

--------------------------------------------------
(8)
Create Folder: Services

Create Interface (File) in Services Folder: IUserService.cs
Create Class (File) in Services Folder: UserService.cs
--------------------------------------------------

--------------------------------------------------
(9)
Create Class (File) in Infrastructure Folder: JwtUtility.cs
--------------------------------------------------

--------------------------------------------------
(10)
Create Folder in Infrastructure Folder: Middlewares
Create Class (File) in Middlewares Folder: JwtMiddleware.cs
--------------------------------------------------

--------------------------------------------------
(11)
Create Class (File) in Controllers Folder: UsersController.cs

Note:
[Infrastructure.Attributes.Authorize] for Get Method (Action)
--------------------------------------------------

--------------------------------------------------
(12)
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

	Get All Users:

		https://localhost:44390/users/getall

	Login:

		(1)
		https://localhost:44390/users/login

		(2)
		https://localhost:44390/users/login

		{"username": "Username1", "password": "temp"}
		{"username": "temp", "password": "1234567890"}

		{"username": "Username1", "password": "1234567890"}

		Check JWT (Token) in https://jwt.io/ Site!

	Get All Users (Secured):

		Check below action with token!

		Postman: Authorization Tab -> Type: Bearer Token -> Token: The token gave by Login!

		https://localhost:44390/users
--------------------------------------------------

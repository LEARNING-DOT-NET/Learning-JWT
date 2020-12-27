using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
	public class Startup : object
	{
		public Startup
			(Microsoft.Extensions.Configuration.IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public Microsoft.Extensions.Configuration.IConfiguration Configuration { get; }

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
	}
}

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

			// Configure Swagger for application services
			services.AddSwaggerGen(current =>
			{
				current.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
				{
					Version = "v1",

					Title = "Swagger Demo",

					Description = "Swagger Demo",

					TermsOfService =
						new System.Uri("https://example.com/terms"),

					Contact =
						new Microsoft.OpenApi.Models.OpenApiContact()
						{
							Name = "Dariush Tasdighi",
							Email = "Dariush.Tasdighi@Gmail.com",
							Url = new System.Uri("https://www.linkedin.com/in/dariush-tasdighi"),
						},

					License =
						new Microsoft.OpenApi.Models.OpenApiLicense
						{
							Name = "Use under LICX",

							Url = new System.Uri("https://example.com/license"),
						},
				});

				current.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
				{
					Name = "Authorization",
					Description = "JWT Authorization header using the Bearer scheme.",

					Scheme = "Bearer",
					In = Microsoft.OpenApi.Models.ParameterLocation.Header,
					Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
				});

				current.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
				{
					{
						new Microsoft.OpenApi.Models.OpenApiSecurityScheme
						{
							Name = "Bearer",
							Scheme = "oauth2",

							In =
								Microsoft.OpenApi.Models.ParameterLocation.Header,

							Reference =
								new Microsoft.OpenApi.Models.OpenApiReference
								{
									Id = "Bearer",
									Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
								},
						},

						new System.Collections.Generic.List<string>()
					}
				});

				var xmlPath =
					System.IO.Path.Combine
					(System.AppContext.BaseDirectory, "Application.xml");

				current.IncludeXmlComments(xmlPath);
			});

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

			// Swagger middleware
			app.UseSwagger();

			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
			});

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}

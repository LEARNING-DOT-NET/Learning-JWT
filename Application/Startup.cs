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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Version = "v1",
                    Title = "Swagger Demo",
                    Description = "Swagger Demo",
                    TermsOfService = new System.Uri("https://example.com/terms"),
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact()
                    {
                        Name = "Dariush Tasdighi",
                        Email = "Dariush.Tasdighi@Gmail.com",
                        Url = new System.Uri("https://www.linkedin.com/in/dariush-tasdighi"),
                    },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new System.Uri("https://example.com/license"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme.",
                    Name = "Authorization",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement()
                {
                    {
                        new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                        {
                            Reference = new Microsoft.OpenApi.Models.OpenApiReference
                            {
                                Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                        },
                        new System.Collections.Generic.List<string>()
                    }
                });

                var xmlPath = System.IO.Path.Combine(System.AppContext.BaseDirectory, "Application.xml");
                c.IncludeXmlComments(xmlPath);

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

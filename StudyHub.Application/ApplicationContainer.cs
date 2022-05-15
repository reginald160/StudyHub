using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Application.Cryptography;
using StudyHub.Application.Filters;
using StudyHub.Application.Settings;
using StudyHub.Infrastructure.Persistence.Repo;
using StudyHub.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ExceptionHandling;

namespace StudyHub.Application
{
	public static class ApplicationContainer 
	{
		
		public static IServiceCollection ApplicationInjectionServices(this IServiceCollection services, IConfiguration configuration)
		{
           
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
			services.AddAutoMapper(typeof(ApplicationContainer));
			services.AddMediatR(typeof(ApplicationContainer));
			services.AddMediatR(Assembly.GetExecutingAssembly());
			services.AddScoped<ICryptographyService, CryptographyService>();
			services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
			services.Configure<JwtSetting>(options => configuration.GetSection("JwtSetting").Bind(options));
			services.Configure<PayPalSettings>(options => configuration.GetSection("PayPalSettings").Bind(options));

			InfrastructureContainer.InfrastructureInjectionServices(services, configuration);
		


			return services;
		}


		public static void Register(HttpConfiguration config)
		{
			//Register it here
			config.Services.Replace(typeof(IExceptionLogger), new UnhandledExceptionLogger());

			// Web API routes
			config.MapHttpAttributeRoutes();

			config.Routes.MapHttpRoute(
				name: "DefaultApi",
				routeTemplate: "api/{controller}/{id}",
				defaults: new { id = RouteParameter.Optional }
			);
		}
		public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
		{
			app.UseMiddleware<ExceptionMiddleware>();
		}


	}
}

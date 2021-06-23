using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

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
		
			InfrastructureContainer.InfrastructureInjectionServices(services, configuration);

			return services;
		}

	}
}

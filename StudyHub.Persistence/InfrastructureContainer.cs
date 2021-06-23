using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace StudyHub.Persistence
{
	public static class InfrastructureContainer
	{
		public static IServiceCollection InfrastructureInjectionServices(this IServiceCollection services, IConfiguration configuration)
		{

			services.AddDbContext<ApplicationDbContext>(options =>
				   options.UseSqlServer(
					   configuration.GetConnectionString("DefaultConnection"), x =>
					   x.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
		
			return services;
		}

	}
}

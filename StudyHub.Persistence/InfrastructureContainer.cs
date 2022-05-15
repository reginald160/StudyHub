using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using StudyHub.Infrastructure.Persistence.Repo;
using System.Configuration;
using StudyHub.Payment;

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

            

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
               
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["JwtSetting:Issuer"],
                    ValidAudience = configuration["JwtSetting:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSetting:Key"]))
                };
              
            });
            
            //services.AddSession(options =>
            //{
            //    options.IdleTimeout = TimeSpan.FromMinutes(30);//We set Time here 
            //    options.Cookie.HttpOnly = true;
            //    options.Cookie.IsEssential = true;

            //});

            services.AddScoped(typeof(DbContext));
            services.AddScoped(typeof(ApplicationDbContext));
            //PaymentStartUp.PaymentInjectionServices(services, configuration);

            return services;
		}

	}
}

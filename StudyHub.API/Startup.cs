using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Microsoft.Owin.Security.Google;
using StudyHub.Application;
using StudyHub.Infrastructure.Persistence.Data;
using StudyHub.Payment;
using StudyHub.Payment.Db;
using StudyHub.Payment.Interfaces;
using StudyHub.Payment.Interfaces.Implementations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace StudyHub.API
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }
		

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			
			services.AddControllers();
			services.AddAutoMapper(typeof(Startup).Assembly);
			var persistenceConnectionString = Configuration.GetConnectionString("DefaultConnection");
			services.AddTransient(x => new DataContext(persistenceConnectionString));
			//services.AddTransient(x => new DataContext(persistenceConnectionString)).;
			// services.AddScoped<IDesignTimeDbContextFactory<DataContext>, DataContextDesignTimeFactory>();

			services.AddScoped<IBankConnectorDomainService, BankConnectorDomainService>();
			services.AddScoped<IMerchantRepository, MerchantRepository>();
			services.AddScoped<IPaymentRepository, PaymentRepository>();
			services.AddScoped<IPaypalServices, PaypalServices>();
			services.AddScoped<ILicenceRepository, LicenceRepository>();

			services.AddSwaggerGen(swagger =>
			{
				swagger.SwaggerDoc("v1", new OpenApiInfo { Title = "StudyHub.API", Version = "v1" });
				swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
				{
					Name = "Authorization",
					Type = SecuritySchemeType.ApiKey,
					Scheme = "Bearer",
					BearerFormat = "JWT",
					In = ParameterLocation.Header,
					Description = "Enter 'Bearer' [space] and then your valid token in the text input below.\r\n\r\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9\"",
				});
				swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
				{
					{
						  new OpenApiSecurityScheme
							{
								Reference = new OpenApiReference
								{
									Type = ReferenceType.SecurityScheme,
									Id = "Bearer"
								}
							},
							new string[] {}

					}
				});

			});
			

			services.AddControllers().AddJsonOptions(jsonOptions =>
			{
				jsonOptions.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
			});
			//services.AddScoped<IMerchantRepository, MerchantRepository>();
			//services.AddScoped<IPaymentRepository, PaymentRepository>();
			ApplicationContainer.ApplicationInjectionServices(services, Configuration);
			


		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			var logger = app.ApplicationServices.GetService<ILogger<Startup>>();

            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });

			//app.UseCors(option => option.WithOrigins("http://localhost:4200", "https://localhost:44319")
			//.AllowAnyMethod()
			//         .AllowAnyHeader());


			        if (env.IsDevelopment())
				app.UseDeveloperExceptionPage();
				//{


				//	app.UseSwagger();
				//	app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyHub.API v1"));
				//}
				//app.ConfigureExceptionHandler(logger);
			//app.ConfigureCustomExceptionMiddleware();
			app.UseSwagger();
			app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "StudyHub.API v1"));
			
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();
			app.UseAuthentication();
			app.UseAuthorization();

           

            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

            //app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            //{
            //    ClientId = "",
            //    ClientSecret = ""
            //});
            //app.UseSession();
            app.UseCors();
			
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
			// update database to newest version via migrations
			// NOTE: in a production-ready system, this should not be executed from the application
			// and instead from a CI/CD or build script. 
			int maxAttempts = 3;
			int attempt = 0;
			while (attempt <= maxAttempts)
			{
				attempt++;

				try
				{
					var dataContext = app.ApplicationServices.GetService<DataContext>();
					//dataContext.Database.Migrate();
					
				}
				catch (SqlException)
				{
					// SQL server might not be available yet, let's retry in a moment

					var waitTimeInSeconds = 5;
					logger.LogWarning($"SQL server is not yet available, retrying in {waitTimeInSeconds} seconds");
					Thread.Sleep(TimeSpan.FromSeconds(waitTimeInSeconds));
				}
			}

			var license = app.ApplicationServices.GetService<ILicenceRepository>();
			//if(license.IsActiveLicence(""))
			//	logger.LogWarning($"Your license is not valid");





		}
	}
}

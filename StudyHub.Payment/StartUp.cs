using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StudyHub.Payment.Db;
using StudyHub.Payment.Interfaces;
using StudyHub.Payment.Interfaces.Implementations;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment
{
    public static class PaymentStartUp
    {
        public static IServiceCollection PaymentInjectionServices(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddScoped(typeof(DataContext));

            //services.AddDbContext<DataContext>(options =>
            //       options.UseSqlServer(
            //           configuration.GetConnectionString("DefaultConnection"), x =>
            //           x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
            var persistenceConnectionString = configuration.GetConnectionString("DefaultConnection");
            services.AddTransient(x => new DataContext(persistenceConnectionString));
            //services.AddTransient(x => new DataContext(persistenceConnectionString)).;
            // services.AddScoped<IDesignTimeDbContextFactory<DataContext>, DataContextDesignTimeFactory>();

            services.AddScoped<IBankConnectorDomainService, BankConnectorDomainService>();
            services.AddScoped<IMerchantRepository, MerchantRepository>();
            services.AddScoped<IPaymentRepository, PaymentRepository>();
            services.AddScoped<IPaypalServices, PaypalServices>();
            services.AddScoped<ILicenceRepository, LicenceRepository>();


            return services;
        }

    }
}

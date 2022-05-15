using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using StudyHub.Payment.Configuration;
using StudyHub.Payment.DomainServices;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyHub.Payment.Db
{
    //public class DataContextDesignTimeFactory : IDesignTimeDbContextFactory<DataContext>
    //{
    //    //public DataContext CreateDbContext(string[] args)
    //    //{
    //    //    // NOTE: This allows to use an SQL DB outside of the environment for fast prototyping.
    //    //    var connectionString = "Data Source=DESKTOP-D8QJHM9;Initial Catalog=StudyHub;Integrated Security=True;Trusted_Connection=True;MultipleActiveResultSets=true";

    //    //    return new DataContext(connectionString);
    //    //}
    //}



    public class DataContext : DbContext
    {
        public DataContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        private readonly string _connectionString;

        public DbSet<PaymentModel> Payments { get; set; }
        public DbSet<Merchant> Merchants { get; set; }
        public DbSet<Licence> Licences { get; set; }
        public DbSet<PaymentOrder> PaymentOrders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PaymentConfiguration());
            modelBuilder.ApplyConfiguration(new MerchantConfiguration());

            //modelBuilder.Entity<Payment>().OwnsOne(p => p.CreditCardInformation);

            base.OnModelCreating(modelBuilder);
        }
    }
}

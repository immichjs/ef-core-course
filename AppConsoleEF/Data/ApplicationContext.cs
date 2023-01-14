using AppConsoleEF.Data.Configurations;
using AppConsoleEF.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppConsoleEF.Data
{
    public class ApplicationContext : DbContext
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(_ => _.AddConsole());

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //string hostname = "IMMICH";
			string hostname = "DESKTOP-O8N03I9";
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer($"Server={hostname}\\SQLEXPRESS;Database=EcommerceEFCoreCourse;User Id=sa;Password=immichjs997905964;Trust Server Certificate=true",
                    _ => _.EnableRetryOnFailure(
                        maxRetryCount: 2,
                        maxRetryDelay: TimeSpan.FromSeconds(5),
                        errorNumbersToAdd: null).MigrationsHistoryTable("curso_ef_core"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }

        private void MappingForgetProperties(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties()
                    .Where(_ => _.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    if (string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}

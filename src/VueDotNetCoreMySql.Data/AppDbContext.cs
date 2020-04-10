using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VueDotNetCoreMySql.Common.Entities;
using VueDotNetCoreMySql.Data.Mappings;

namespace VueDotNetCoreMySql.Data
{
    public class AppDbContext : DbContext
    {
        bool isDevelopment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

        protected override void OnConfiguring(DbContextOptionsBuilder dbContextBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            dbContextBuilder.UseMySQL(configuration.GetConnectionString("DefaultConnection"));
        }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new UserMapping());
            builder.ApplyConfiguration(new RoleMapping());

            if (isDevelopment)
                builder.Seed();
        }
    }
}
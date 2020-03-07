using Microsoft.EntityFrameworkCore;
using MySql.Data.EntityFrameworkCore.Extensions;
using PPR.Data.Entities;
using PPR.Data.Mappings;

namespace PPR.Data {
    public class AppDbContext : DbContext {
        protected override void OnConfiguring (DbContextOptionsBuilder optionsBuilder) {
            optionsBuilder.UseMySQL ("server=localhost;database=test;user=root;password=redhat");
        }

        public DbSet<Book> Book { get; set; }

        public DbSet<Publisher> Publisher { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating (ModelBuilder builder) {
            base.OnModelCreating (builder);

            builder.Entity<Publisher> (entity => {
                entity.HasKey (e => e.ID);
                entity.Property (e => e.Name).IsRequired ();
            });

            builder.Entity<Book> (entity => {
                entity.HasKey (e => e.ISBN);
                entity.Property (e => e.Title).IsRequired ();
                entity.HasOne (d => d.Publisher)
                    .WithMany (p => p.Books);
            });

            builder.ApplyConfiguration (new UserMapping ());
            builder.ApplyConfiguration (new RoleMapping ());
        }
    }
}
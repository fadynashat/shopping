using Microsoft.EntityFrameworkCore;

using Shooping.models;
using Shooping.Models;

namespace Shooping
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)

        {
                optionsBuilder.EnableSensitiveDataLogging();

            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory) 
                    .AddJsonFile("appsettings.json")
                    .Build();

                optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
         
            }
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
             .Property(p => p.Id)
             .ValueGeneratedOnAdd();

            modelBuilder.Entity<Product>()
                .HasKey(p => p.Id); // Specifies the primary key
            // Configure CartItem entity
            modelBuilder.Entity<CartItem>(entity =>
            {
                // Configure primary key, relationships, indexes, etc.
                // Example:
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                // Add other configurations as needed
            });

            // Call base method if necessary
            base.OnModelCreating(modelBuilder);
        }
    }
}

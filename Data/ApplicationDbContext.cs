using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }  // For auth

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Primary keys
            modelBuilder.Entity<Product>().HasKey(p => p.Id);
            modelBuilder.Entity<User>().HasKey(u => u.Id);

            //  Decimal precision fix (Price)
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            // ✅ Static seed data (deterministic)
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Email = "nazim@milkyway.com",
                    PasswordHash = "$2b$10$yRcac5XzNqWBrKZLY3pr2uD5IQHLc43OydI5P1HL0vuFSWf6iJHkK", //password123
                    Role = "Manager"
                }
            );
        }
    }
}

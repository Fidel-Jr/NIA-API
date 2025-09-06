using Microsoft.EntityFrameworkCore;
using Nia.Domain.Entities;

namespace Nia.Infrastructure.Data
{
    public class NiaDbContext : DbContext
    {
        public NiaDbContext(DbContextOptions<NiaDbContext> options)
        : base(options)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<QRCode>().HasKey(qr => new
            {
                qr.Guid,
            });
            modelBuilder.Entity<ItemArchive>().HasKey(ia => new
            {
                ia.ItemId,
            });
            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, CategoryName = "Desktop", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Category { Id = 2, CategoryName = "Consumables", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Category { Id = 3, CategoryName = "ICT", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) }
            );
            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, LocationName = "Billing Unit", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Location { Id = 2, LocationName = "Engineering", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Location { Id = 3, LocationName = "O & M", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Location { Id = 4, LocationName = "Admin & Finance", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Location { Id = 5, LocationName = "IDDD", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Location { Id = 6, LocationName = "Equipment", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) }
            );
            modelBuilder.Entity<Condition>().HasData(
                new Condition { Id = 1, ConditionName = "Serviceable", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Condition { Id = 2, ConditionName = "Non - Serviceable", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new Condition { Id = 3, ConditionName = "On Maintenance", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) }
            );
            modelBuilder.Entity<ConditionNumber>().HasData(
                new ConditionNumber { Id = 1, ConditionNumberType = "A1", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new ConditionNumber { Id = 2, ConditionNumberType = "A2", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new ConditionNumber { Id = 3, ConditionNumberType = "A3", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new ConditionNumber { Id = 4, ConditionNumberType = "A4" , CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) },
                new ConditionNumber { Id = 5, ConditionNumberType = "A5", CreatedAt = new DateTime(2025, 07, 17), UpdatedAt = new DateTime(2025, 07, 17) }
            );
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Alice Mendoza",
                    Username = "alice.admin",
                    PasswordHash = "AdminPass123", // Consider hashing in real apps
                    Role = "Admin",
                    ImagePath = "/images/users/alice.png",
                    LocationId = 1,
                    CreatedAt = new DateTime(2025, 07, 17),
                    UpdatedAt = new DateTime(2025, 07, 17)
                },
                new User
                {
                    Id = 2,
                    FullName = "John Reyes",
                    Username = "john.reyes",
                    PasswordHash = "UserPass123",
                    Role = "User",
                    ImagePath = "/images/users/john.png",
                    LocationId = 2,
                    CreatedAt = new DateTime(2025, 07, 17),
                    UpdatedAt = new DateTime(2025, 07, 17)
                }
            );
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<QRCode> QRCodes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Condition> Conditions { get; set; }
        public DbSet<ConditionNumber> ConditionNumbers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<BorrowTransaction> BorrowTransactions { get; set; }
        public DbSet<ItemConditionHistory> ItemConditionHistories { get; set; }
    }
}

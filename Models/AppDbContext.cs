using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().HasData(
            new User { Id = 1, Username = "admin1", Password = "admin123", Role = "Admin" },
            new User { Id = 2, Username = "admin2", Password = "admin456", Role = "Admin" },
            new User { Id = 3, Username = "user1", Password = "user123", Role = "User" },
            new User { Id = 4, Username = "user2", Password = "user456", Role = "User" }
        );
        modelBuilder.Entity<Contact>().HasData(
            new Contact { Id = 1, Name = "Иванов Иван", Phone = "+7 900 111-22-33", Category = "Семья", OwnerUsername = "user1" },
            new Contact { Id = 2, Name = "Петров Пётр", Phone = "+7 901 222-33-44", Category = "Работа", OwnerUsername = "admin1" }
        );

    }

    public DbSet<Contact> Contacts => Set<Contact>();
    public DbSet<Category> Categories => Set<Category>();
}

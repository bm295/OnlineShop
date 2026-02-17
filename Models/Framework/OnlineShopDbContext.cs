using Microsoft.EntityFrameworkCore;

namespace Models.Framework;

public class OnlineShopDbContext(DbContextOptions<OnlineShopDbContext> options) : DbContext(options)
{
    public OnlineShopDbContext()
    {
    }

    public virtual DbSet<Category> Categories => Set<Category>();
    public virtual DbSet<Product> Products => Set<Product>();
    public virtual DbSet<Account> Accounts => Set<Account>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=OnlineShopDb;Trusted_Connection=True;TrustServerCertificate=True;");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>().HasKey(a => new { a.UserName, a.Password });

        modelBuilder.Entity<Category>()
            .Property(e => e.Alias)
            .IsUnicode(false);

        modelBuilder.Entity<Product>()
            .Property(e => e.Alias)
            .IsUnicode(false);

        modelBuilder.Entity<Product>()
            .Property(e => e.Price)
            .HasPrecision(18, 0);

        modelBuilder.Entity<Account>()
            .Property(e => e.UserName)
            .IsUnicode(false);

        modelBuilder.Entity<Account>()
            .Property(e => e.Password)
            .IsUnicode(false);
    }
}

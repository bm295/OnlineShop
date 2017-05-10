using System.Data.Entity;

namespace Models.Framework
{
    public class OnlineShopDbContext : DbContext
    {
        public OnlineShopDbContext()
            : base("OnlineShopDbContext")
        {
        }

        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
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
}

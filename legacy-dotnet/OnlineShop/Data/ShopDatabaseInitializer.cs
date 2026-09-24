using OnlineShop.Models;

namespace OnlineShop.Data;

public sealed class ShopDatabaseInitializer(ShopDbContext db, TimeProvider timeProvider)
{
    public void Initialize()
    {
        db.Database.EnsureCreated();

        if (db.Categories.Any())
        {
            return;
        }

        var pantry = new Category { Name = "Pantry", Description = "Shelf-stable grocery items" };
        var produce = new Category { Name = "Produce", Description = "Fresh fruits and vegetables" };

        db.Categories.AddRange(pantry, produce);
        db.Products.AddRange(
            new Product { Name = "Olive Oil", Sku = "PAN-001", Price = 14.99m, StockQuantity = 34, Category = pantry },
            new Product { Name = "Brown Rice", Sku = "PAN-002", Price = 7.49m, StockQuantity = 50, Category = pantry },
            new Product { Name = "Apples", Sku = "PRO-001", Price = 3.25m, StockQuantity = 80, Category = produce });

        db.Orders.Add(new Order
        {
            CustomerName = "Walk-in Customer",
            CreatedAtUtc = timeProvider.GetUtcNow().UtcDateTime,
            TotalAmount = 22.48m,
            Status = OrderStatus.Completed
        });

        db.SaveChanges();
    }
}

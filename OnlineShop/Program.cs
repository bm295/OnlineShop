using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<ShopDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("ShopDb") ?? "Data Source=shop.db"));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
    db.Database.EnsureCreated();

    if (!db.Categories.Any())
    {
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
            CreatedAtUtc = DateTime.UtcNow,
            TotalAmount = 22.48m,
            Status = OrderStatus.Completed
        });

        db.SaveChanges();
    }
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Dashboard}/{action=Index}/{id?}");

app.Run();

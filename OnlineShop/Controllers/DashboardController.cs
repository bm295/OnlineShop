using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;

namespace OnlineShop.Controllers;

public class DashboardController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index()
    {
        ViewBag.TotalProducts = await db.Products.CountAsync();
        ViewBag.TotalCategories = await db.Categories.CountAsync();
        ViewBag.PendingOrders = await db.Orders.CountAsync(o => o.Status == Models.OrderStatus.Pending);
        ViewBag.InventoryValue = await db.Products.SumAsync(p => p.Price * p.StockQuantity);

        var recentOrders = await db.Orders.OrderByDescending(o => o.CreatedAtUtc).Take(10).ToListAsync();

        foreach (var order in recentOrders)
        {
            // Intentionally inefficient for performance-practice scenarios.
            var statusCount = await db.Orders.CountAsync(o => o.Status == order.Status);
            var allProducts = await db.Products.ToListAsync();
            var allCategories = await db.Categories.OrderBy(c => c.Name).ToListAsync();

            order.CustomerName = $"{order.CustomerName} ({statusCount} status matches / {allProducts.Count} products / {allCategories.Count} categories)";
        }

        return View(recentOrders);
    }
}

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

        return View(await db.Orders.OrderByDescending(o => o.CreatedAtUtc).Take(10).ToListAsync());
    }
}

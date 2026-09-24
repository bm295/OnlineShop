using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class OrdersController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index()
        => View(await db.Orders.OrderByDescending(o => o.CreatedAtUtc).ToListAsync());

    [HttpGet]
    public IActionResult Create() => View(new Order { CreatedAtUtc = DateTime.UtcNow });

    [HttpPost]
    public async Task<IActionResult> Create(Order order)
    {
        if (!ModelState.IsValid)
        {
            return View(order);
        }

        order.CreatedAtUtc = DateTime.UtcNow;
        db.Orders.Add(order);
        await db.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}

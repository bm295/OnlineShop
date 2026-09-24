using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class CategoriesController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index() => View(await db.Categories.OrderBy(c => c.Name).ToListAsync());

    [HttpGet]
    public IActionResult Create() => View(new Category());

    [HttpPost]
    public async Task<IActionResult> Create(Category category)
    {
        if (!ModelState.IsValid)
        {
            return View(category);
        }

        db.Categories.Add(category);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }
}

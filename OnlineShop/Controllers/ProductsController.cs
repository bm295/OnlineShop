using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class ProductsController(ShopDbContext db) : Controller
{
    public async Task<IActionResult> Index()
        => View(await db.Products.Include(p => p.Category).OrderBy(p => p.Name).ToListAsync());

    [HttpGet]
    public async Task<IActionResult> Create()
    {
        await PopulateCategoriesAsync();
        return View(new Product());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync();
            return View(product);
        }

        db.Products.Add(product);
        await db.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync()
    {
        var categories = await db.Categories.OrderBy(c => c.Name).ToListAsync();
        ViewBag.Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
    }
}

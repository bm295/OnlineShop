using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using OnlineShop.Application;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class ProductsController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await sender.Send(new ListProductsQuery(), cancellationToken));

    [HttpGet]
    public async Task<IActionResult> Create(CancellationToken cancellationToken)
    {
        await PopulateCategoriesAsync(cancellationToken);
        return View(new Product());
    }

    [HttpPost]
    public async Task<IActionResult> Create(Product product, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
        {
            await PopulateCategoriesAsync(cancellationToken);
            return View(product);
        }

        await sender.Send(new CreateProductCommand(product), cancellationToken);
        return RedirectToAction(nameof(Index));
    }

    private async Task PopulateCategoriesAsync(CancellationToken cancellationToken)
    {
        var categories = await sender.Send(new ListProductCategoriesQuery(), cancellationToken);
        ViewBag.Categories = new SelectList(categories, nameof(Category.Id), nameof(Category.Name));
    }
}

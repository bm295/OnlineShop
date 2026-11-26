using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class CategoriesController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await sender.Send(new ListCategoriesQuery(), cancellationToken));

    [HttpGet]
    public IActionResult Create() => View(new Category());

    [HttpPost]
    public async Task<IActionResult> Create(Category category, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(category);

        await sender.Send(new CreateCategoryCommand(category), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}

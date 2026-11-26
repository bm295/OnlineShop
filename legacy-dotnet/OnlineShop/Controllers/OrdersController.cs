using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application;
using OnlineShop.Models;

namespace OnlineShop.Controllers;

public class OrdersController(ISender sender, TimeProvider clock) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
        => View(await sender.Send(new ListOrdersQuery(), cancellationToken));

    [HttpGet]
    public IActionResult Create() => View(new Order { CreatedAtUtc = clock.GetUtcNow().UtcDateTime });

    [HttpPost]
    public async Task<IActionResult> Create(Order order, CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return View(order);

        await sender.Send(new CreateOrderCommand(order), cancellationToken);
        return RedirectToAction(nameof(Index));
    }
}

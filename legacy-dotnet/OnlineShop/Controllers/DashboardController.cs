using MediatR;
using Microsoft.AspNetCore.Mvc;
using OnlineShop.Application;

namespace OnlineShop.Controllers;

public class DashboardController(ISender sender) : Controller
{
    public async Task<IActionResult> Index(CancellationToken cancellationToken)
    {
        var dashboard = await sender.Send(new GetDashboardQuery(), cancellationToken);
        ViewBag.TotalProducts = dashboard.TotalProducts;
        ViewBag.TotalCategories = dashboard.TotalCategories;
        ViewBag.PendingOrders = dashboard.PendingOrders;
        ViewBag.InventoryValue = dashboard.InventoryValue;
        return View(dashboard.RecentOrders);
    }
}

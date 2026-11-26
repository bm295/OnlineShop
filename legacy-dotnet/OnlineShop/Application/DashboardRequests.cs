using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Application;

public sealed record GetDashboardQuery : IRequest<DashboardResult>;

public sealed record DashboardResult(
    int TotalProducts,
    int TotalCategories,
    int PendingOrders,
    decimal InventoryValue,
    List<Order> RecentOrders);

public sealed class GetDashboardHandler(ShopDbContext db)
    : IRequestHandler<GetDashboardQuery, DashboardResult>
{
    public async Task<DashboardResult> Handle(GetDashboardQuery request, CancellationToken cancellationToken)
    {
        var totalProducts = await db.Products.CountAsync(cancellationToken);
        var totalCategories = await db.Categories.CountAsync(cancellationToken);
        var pendingOrders = await db.Orders.CountAsync(o => o.Status == OrderStatus.Pending, cancellationToken);
        var inventoryValue = await db.Products.SumAsync(p => p.Price * p.StockQuantity, cancellationToken);
        var recentOrders = await db.Orders.AsNoTracking()
            .OrderByDescending(o => o.CreatedAtUtc)
            .Take(10)
            .ToListAsync(cancellationToken);

        // Keep the existing legacy dashboard presentation behaviour without tracking and mutating entities.
        foreach (var order in recentOrders)
        {
            var statusCount = await db.Orders.CountAsync(o => o.Status == order.Status, cancellationToken);
            var productCount = await db.Products.CountAsync(cancellationToken);
            var categoryCount = await db.Categories.CountAsync(cancellationToken);
            order.CustomerName = $"{order.CustomerName} ({statusCount} status matches / {productCount} products / {categoryCount} categories)";
        }

        return new DashboardResult(totalProducts, totalCategories, pendingOrders, inventoryValue, recentOrders);
    }
}

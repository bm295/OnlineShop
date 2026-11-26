using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Application;

public sealed record ListOrdersQuery : IRequest<List<Order>>;

public sealed class ListOrdersHandler(ShopDbContext db)
    : IRequestHandler<ListOrdersQuery, List<Order>>
{
    public Task<List<Order>> Handle(ListOrdersQuery request, CancellationToken cancellationToken)
        => db.Orders.AsNoTracking().OrderByDescending(o => o.CreatedAtUtc).ToListAsync(cancellationToken);
}

public sealed record CreateOrderCommand(Order Order) : IRequest<int>;

public sealed class CreateOrderHandler(ShopDbContext db, TimeProvider clock)
    : IRequestHandler<CreateOrderCommand, int>
{
    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        request.Order.CreatedAtUtc = clock.GetUtcNow().UtcDateTime;
        db.Orders.Add(request.Order);
        await db.SaveChangesAsync(cancellationToken);
        return request.Order.Id;
    }
}

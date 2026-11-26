using MediatR;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using OnlineShop.Application;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Tests.Application;

public class MediatRHandlersTests
{
    [Fact]
    public async Task CommandsAndQueries_RunThroughMediator_AndPersistData()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();

        var services = new ServiceCollection();
        services.AddLogging();
        services.AddDbContext<ShopDbContext>(options => options.UseSqlite(connection));
        services.AddSingleton(TimeProvider.System);
        services.AddMediatR(configuration =>
            configuration.RegisterServicesFromAssemblyContaining<CreateCategoryCommand>());
        using var provider = services.BuildServiceProvider();

        using var scope = provider.CreateScope();
        var db = scope.ServiceProvider.GetRequiredService<ShopDbContext>();
        await db.Database.EnsureCreatedAsync();
        var sender = scope.ServiceProvider.GetRequiredService<ISender>();

        var category = new Category { Name = "Books" };
        var categoryId = await sender.Send(new CreateCategoryCommand(category));
        Assert.True(categoryId > 0);

        var productId = await sender.Send(new CreateProductCommand(new Product
        {
            Name = "Architecture Book",
            Sku = "BOOK-001",
            CategoryId = categoryId,
            Price = 20m,
            StockQuantity = 4
        }));
        Assert.True(productId > 0);

        var orderId = await sender.Send(new CreateOrderCommand(new Order
        {
            CustomerName = "Customer",
            TotalAmount = 20m,
            Status = OrderStatus.Pending
        }));
        Assert.True(orderId > 0);

        Assert.Single(await sender.Send(new ListCategoriesQuery()));
        Assert.Single(await sender.Send(new ListProductCategoriesQuery()));
        Assert.Single(await sender.Send(new ListProductsQuery()));
        var orders = await sender.Send(new ListOrdersQuery());
        Assert.Single(orders);
        Assert.True(orders[0].CreatedAtUtc > DateTime.MinValue);

        var dashboard = await sender.Send(new GetDashboardQuery());
        Assert.Equal(1, dashboard.TotalProducts);
        Assert.Equal(1, dashboard.TotalCategories);
        Assert.Equal(1, dashboard.PendingOrders);
        Assert.Equal(80m, dashboard.InventoryValue);
        Assert.Single(dashboard.RecentOrders);
    }

    [Fact]
    public async Task CreateOrderCommand_UsesInjectedTimeProvider()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        await connection.OpenAsync();
        var options = new DbContextOptionsBuilder<ShopDbContext>().UseSqlite(connection).Options;
        await using var db = new ShopDbContext(options);
        await db.Database.EnsureCreatedAsync();

        var clock = new FixedTimeProvider(new DateTimeOffset(2026, 11, 26, 10, 0, 0, TimeSpan.Zero));
        var handler = new CreateOrderHandler(db, clock);
        var order = new Order { CustomerName = "Customer", TotalAmount = 5m };
        await handler.Handle(new CreateOrderCommand(order), CancellationToken.None);

        Assert.Equal(clock.GetUtcNow().UtcDateTime, order.CreatedAtUtc);
    }

    private sealed class FixedTimeProvider(DateTimeOffset now) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => now;
    }
}

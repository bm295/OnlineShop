using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Tests.Data;

public sealed class ShopDatabaseInitializerTests
{
    [Fact]
    public void Initialize_creates_the_expected_shop_data()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateContext(connection);
        var createdAt = new DateTimeOffset(2025, 4, 3, 2, 1, 0, TimeSpan.Zero);

        new ShopDatabaseInitializer(db, new FixedTimeProvider(createdAt)).Initialize();

        Assert.Equal(new[] { "Pantry", "Produce" }, db.Categories.OrderBy(category => category.Name).Select(category => category.Name));
        Assert.Equal(new[] { "Apples", "Brown Rice", "Olive Oil" }, db.Products.OrderBy(product => product.Name).Select(product => product.Name));
        var order = Assert.Single(db.Orders);
        Assert.Equal("Walk-in Customer", order.CustomerName);
        Assert.Equal(22.48m, order.TotalAmount);
        Assert.Equal(OrderStatus.Completed, order.Status);
        Assert.Equal(createdAt.UtcDateTime, order.CreatedAtUtc);
    }

    [Fact]
    public void Initialize_does_not_duplicate_data_when_the_shop_is_already_initialized()
    {
        using var connection = new SqliteConnection("Data Source=:memory:");
        connection.Open();
        using var db = CreateContext(connection);
        var initializer = new ShopDatabaseInitializer(db, new FixedTimeProvider(DateTimeOffset.UnixEpoch));

        initializer.Initialize();
        initializer.Initialize();

        Assert.Equal(2, db.Categories.Count());
        Assert.Equal(3, db.Products.Count());
        Assert.Single(db.Orders);
    }

    private static ShopDbContext CreateContext(SqliteConnection connection)
        => new(new DbContextOptionsBuilder<ShopDbContext>().UseSqlite(connection).Options);

    private sealed class FixedTimeProvider(DateTimeOffset utcNow) : TimeProvider
    {
        public override DateTimeOffset GetUtcNow() => utcNow;
    }
}

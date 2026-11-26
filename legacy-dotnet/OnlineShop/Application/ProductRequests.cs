using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Application;

public sealed record ListProductsQuery : IRequest<List<Product>>;

public sealed class ListProductsHandler(ShopDbContext db)
    : IRequestHandler<ListProductsQuery, List<Product>>
{
    public Task<List<Product>> Handle(ListProductsQuery request, CancellationToken cancellationToken)
        => db.Products.AsNoTracking().Include(p => p.Category).OrderBy(p => p.Name).ToListAsync(cancellationToken);
}

public sealed record ListProductCategoriesQuery : IRequest<List<Category>>;

public sealed class ListProductCategoriesHandler(ShopDbContext db)
    : IRequestHandler<ListProductCategoriesQuery, List<Category>>
{
    public Task<List<Category>> Handle(ListProductCategoriesQuery request, CancellationToken cancellationToken)
        => db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync(cancellationToken);
}

public sealed record CreateProductCommand(Product Product) : IRequest<int>;

public sealed class CreateProductHandler(ShopDbContext db)
    : IRequestHandler<CreateProductCommand, int>
{
    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        db.Products.Add(request.Product);
        await db.SaveChangesAsync(cancellationToken);
        return request.Product.Id;
    }
}

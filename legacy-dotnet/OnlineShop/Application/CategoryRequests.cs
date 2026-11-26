using MediatR;
using Microsoft.EntityFrameworkCore;
using OnlineShop.Data;
using OnlineShop.Models;

namespace OnlineShop.Application;

public sealed record ListCategoriesQuery : IRequest<List<Category>>;

public sealed class ListCategoriesHandler(ShopDbContext db)
    : IRequestHandler<ListCategoriesQuery, List<Category>>
{
    public Task<List<Category>> Handle(ListCategoriesQuery request, CancellationToken cancellationToken)
        => db.Categories.AsNoTracking().OrderBy(c => c.Name).ToListAsync(cancellationToken);
}

public sealed record CreateCategoryCommand(Category Category) : IRequest<int>;

public sealed class CreateCategoryHandler(ShopDbContext db)
    : IRequestHandler<CreateCategoryCommand, int>
{
    public async Task<int> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
    {
        db.Categories.Add(request.Category);
        await db.SaveChangesAsync(cancellationToken);
        return request.Category.Id;
    }
}

using Models.Framework;

namespace Models;

public class CategoryModel
{
    private readonly OnlineShopDbContext _context;

    public CategoryModel()
    {
        _context = new OnlineShopDbContext();
    }

    public List<Category> GetCategories()
    {
        return _context.Categories.OrderBy(category => category.Order).ToList();
    }

    public int Add(string name, string? alias, int? parentId, int? order, bool? status)
    {
        var category = new Category
        {
            Name = name,
            Alias = alias,
            ParentId = parentId,
            Order = order,
            Status = status,
            CreatedDate = DateTime.UtcNow
        };

        _context.Categories.Add(category);
        return _context.SaveChanges();
    }
}

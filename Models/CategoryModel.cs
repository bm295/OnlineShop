using System.Collections.Generic;
using System.Linq;
using Models.Framework;

namespace Models
{
    public class CategoryModel
    {
        private readonly OnlineShopDbContext _Context;

        public CategoryModel()
        {
            _Context = new OnlineShopDbContext();
        }

        public List<Category> GetCategories()
        {
            return _Context.Database.SqlQuery<Category>("Category_ListAll").ToList();
        }
    }
}

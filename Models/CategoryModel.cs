using System.Collections.Generic;
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

        }
    }
}

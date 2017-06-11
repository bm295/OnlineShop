using System.Collections.Generic;
using System.Data.SqlClient;
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

        public int Add(string name, string alias, int? parentId, int? order, bool? status)
        {
            object[] parameters =
            {
                new SqlParameter("@name", name),
                new SqlParameter("@alias", alias),
                new SqlParameter("@parentId", parentId),
                new SqlParameter("@order", order),
                new SqlParameter("@status", status)
            };
            return _Context.Database.ExecuteSqlCommand("exec Category_Add @name, @alias, @parentId, @order, @status", parameters);
        }
    }
}

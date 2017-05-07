using System.Data.SqlClient;
using System.Linq;
using Models.Framework;

namespace Models
{
    public class AccountModel
    {
        private readonly OnlineShopDbContext _Context;

        public AccountModel()
        {
            _Context = new OnlineShopDbContext();
        }

        public bool Login(string userName, string password)
        {
            object[] sqlParams = {
                new SqlParameter("@userName", userName),
                new SqlParameter("@password", password) 
            };
            return _Context.Database.SqlQuery<bool>("exec [Account_Login] @userName, @password", sqlParams).SingleOrDefault();
        }
    }
}

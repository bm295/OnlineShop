using Models.Framework;

namespace Models;

public class AccountModel
{
    private readonly OnlineShopDbContext _context;

    public AccountModel()
    {
        _context = new OnlineShopDbContext();
    }

    public bool Login(string userName, string password)
    {
        return _context.Accounts.Any(account => account.UserName == userName && account.Password == password);
    }
}

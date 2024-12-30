using spendo_be.Context;
using spendo_be.Models;

namespace spendo_be.Services;

public class AccountService : IAccountService
{
    private readonly SpendoContext _context = new();
    
    public Account CreateAccount(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public List<Account> GetAccounts(int userId)
    {
        var accounts = _context.Accounts.Where(a => a.Userid == userId).ToList();
        return accounts;
    }

    public Account UpdateAccount(Account account)
    {
        _context.Accounts.Update(account);
        _context.SaveChanges();
        return account;
    }

    public Account DeleteAccount(int id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null) _context.Accounts.Remove(account);
        return account;
    }
}
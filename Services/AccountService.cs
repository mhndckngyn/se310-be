using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public class AccountService : IAccountService
{
    private readonly SpendoContext _context = new();
    
    public Account CreateAccount(string accountName)
    {
        var account = new Account { Name = accountName };
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public List<AccountDto> GetAccounts(int userId)
    {
        var accounts = _context.Accounts.Where(a => a.Userid == userId)
            .Select(a => new AccountDto
            {
                Id = a.Id,
                Name = a.Name,
                Balance = a.Balance
            }).ToList();
        return accounts;
    }

    public Account UpdateAccount(AccountUpdateDto newAccountInfo)
    {
        var account = _context.Accounts.FirstOrDefault(a => a.Id == newAccountInfo.Id);
        if (newAccountInfo.Name != null && account.Name != newAccountInfo.Name)
        {
            account.Name = newAccountInfo.Name;
        }
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
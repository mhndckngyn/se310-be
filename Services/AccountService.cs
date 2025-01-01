using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public class AccountService : IAccountService
{
    private readonly SpendoContext _context = new();
    
    public Account CreateAccount(int userId, AccountCreateDto accountInfo)
    {
        var account = new Account { Name = accountInfo.Name, Balance = accountInfo.Balance, Userid = userId };
        _context.Accounts.Add(account);
        _context.SaveChanges();
        return account;
    }

    public AccountDto? GetAccountById(int id)
    {
        var account = _context.Accounts.Where(a => a.Id == id)
                        .Select(a => new AccountDto
                        {
                            Id = a.Id,
                            Name = a.Name,
                            Balance = a.Balance
                        })
                        .FirstOrDefault();
        
        return account;
    }

    public List<AccountDto> GetAccountsByUser(int userId)
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

    public Account? UpdateAccount(int id, string accountName)
    {
        var account = _context.Accounts.Find(id);

        if (account == null)
        {
            return null;
        }

        account.Name = accountName;
        _context.Accounts.Update(account);
        _context.SaveChanges();
        return account;
    }

    public Account? DeleteAccount(int id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
        }
        return account;
    }
}
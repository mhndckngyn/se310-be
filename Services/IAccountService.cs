using spendo_be.Models;

namespace spendo_be.Services;

public interface IAccountService
{
    public Account CreateAccount(Account account);
    public List<Account> GetAccounts(int userId);
    public Account UpdateAccount(Account account);
    public Account DeleteAccount(int id);
}
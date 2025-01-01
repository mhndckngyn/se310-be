using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public interface IAccountService
{
    public Account CreateAccount(int userId, string accountName);
    public AccountDto? GetAccountById(int id);
    public List<AccountDto> GetAccountsByUser(int userId);
    public Account? UpdateAccount(int id, string accountName);
    public Account? DeleteAccount(int id);
}
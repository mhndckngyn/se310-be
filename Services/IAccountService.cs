using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public interface IAccountService
{
    public Account CreateAccount(string accountName);
    public List<AccountDto> GetAccounts(int userId);
    public Account UpdateAccount(AccountUpdateDto accountUpdateDto);
    public Account DeleteAccount(int id);
}
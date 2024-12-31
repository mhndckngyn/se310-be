using spendo_be.Models;

namespace spendo_be.Services;

public interface ICurrencyService
{
    public List<Currency> GetCurrencies();
    public Currency? GetCurrencyById(int id);
}
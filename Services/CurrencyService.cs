using spendo_be.Context;
using spendo_be.Models;

namespace spendo_be.Services;

public class CurrencyService : ICurrencyService
{
    private readonly SpendoContext _context = new();
    
    public List<Currency> GetCurrencies()
    {
        return _context.Currencies.ToList();
    }

    public Currency? GetCurrencyById(int id)
    {
        return _context.Currencies.Find(id);
    }
}
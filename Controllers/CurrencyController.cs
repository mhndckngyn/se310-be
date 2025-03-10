using Microsoft.AspNetCore.Mvc;
using spendo_be.Services;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class CurrencyController : ControllerBase
{
    private readonly ICurrencyService _currencyService;

    public CurrencyController(ICurrencyService currencyService)
    {
        _currencyService = currencyService;
    }

    [HttpGet]
    public IActionResult GetCurrencies()
    {
        var currencies = _currencyService.GetCurrencies();
        return Ok(currencies);
    }

    [HttpGet("{id:int}")]
    public IActionResult GetCurrencyById([FromRoute] int id)
    {
        var currency = _currencyService.GetCurrencyById(id);
        if (currency == null)
        {
            return NotFound();
        }
        
        return Ok(currency);
    }
}
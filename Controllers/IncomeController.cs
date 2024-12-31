using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spendo_be.Models.DTO;
using spendo_be.Services;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class IncomeController : ControllerBase
{
    private readonly IIncomeService _incomeService;

    public IncomeController(IIncomeService incomeService)
    {
        _incomeService = incomeService;
    }

    [HttpGet]
    [Authorize]
    public IActionResult GetIncomes(
        [FromQuery] string[] accountIds, 
        [FromQuery] string[] categoryIds, 
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        
        var accountIdsAsInts = accountIds
            .Where(id => int.TryParse(id, out _))
            .Select(int.Parse)
            .ToArray();
        
        var categoryIdsAsInts = categoryIds
            .Where(id => int.TryParse(id, out _))
            .Select(int.Parse)
            .ToArray();

        var criteria = new RecordQueryCriteria
        {
            UserId = userId,
            AccountIds = accountIdsAsInts,
            CategoryIds = categoryIdsAsInts,
            StartDate = startDate,
            EndDate = endDate
        };
        
        var incomes = _incomeService.GetListIncomeByCriteria(criteria);
        return Ok(incomes);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateIncome([FromBody] IncomeCreateDto incomeInfo)
    {
        _incomeService.CreateIncome(incomeInfo);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateIncome([FromBody] IncomeUpdateDto incomeInfo)
    {
        _incomeService.UpdateIncome(incomeInfo);
        return Ok();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public IActionResult DeleteIncome([FromRoute] int id)
    {
        _incomeService.DeleteIncome(id);
        return Ok();
    }
}
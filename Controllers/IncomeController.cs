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

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetIncomeById(int id)
    {
        var income = _incomeService.GetIncomeById(id);
        if (income == null)
        {
            return NotFound();
        }
        
        return Ok(income);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetIncomes(
        [FromQuery] string[] accountIds, 
        [FromQuery] string[] categoryIds, 
        [FromQuery] DateTime? startDate, 
        [FromQuery] DateTime? endDate)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized();
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

    [Authorize]
    [HttpPost]
    public IActionResult CreateIncome([FromBody] IncomeCreateDto incomeInfo)
    {
        var income = _incomeService.CreateIncome(incomeInfo);
        return CreatedAtAction(nameof(GetIncomeById), new { id = income.Id }, income);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult UpdateIncome([FromRoute] int id ,[FromBody] IncomeUpdateDto incomeInfo)
    {
        var updatedIncome = _incomeService.UpdateIncome(id, incomeInfo);
        
        if (updatedIncome == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteIncome([FromRoute] int id)
    {
        var deletedIncome = _incomeService.DeleteIncome(id);

        if (deletedIncome == null)
        {
            return NotFound();
        }
        
        return Ok();
    }
}
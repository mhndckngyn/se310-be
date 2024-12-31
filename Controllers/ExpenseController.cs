using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spendo_be.Models.DTO;
using spendo_be.Services;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class ExpenseController : ControllerBase
{
    private readonly IExpenseService _expenseService;

    public ExpenseController(IExpenseService expenseService)
    {
        _expenseService = expenseService;
    }

    [Authorize]
    [HttpGet("${id:int}")]
    public IActionResult GetExpenseById(int id)
    {
        var expense = _expenseService.GetExpenseById(id);
        if (expense == null)
        {
            return NotFound();
        }
        
        return Ok(expense);
    }
    
    [Authorize]
    [HttpGet]
    public IActionResult GetExpenses(        
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
        
        var expenses = _expenseService.GetListExpenseByCriteria(criteria);
        return Ok(expenses);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult CreateExpense([FromBody] ExpenseCreateDto incomeInfo)
    {
        var expense = _expenseService.CreateExpense(incomeInfo);
        return CreatedAtAction(nameof(GetExpenseById), new { id = expense.Id }, expense);
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateIncome([FromBody] ExpenseUpdateDto incomeInfo)
    {
        var updatedExpense = _expenseService.UpdateExpense(incomeInfo);

        if (updatedExpense == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [HttpDelete("{id:int}")]
    [Authorize]
    public IActionResult DeleteIncome([FromRoute] int id)
    {
        var deletedExpense = _expenseService.DeleteExpense(id);

        if (deletedExpense == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
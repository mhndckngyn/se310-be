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
        
        var expenses = _expenseService.GetListExpenseByCriteria(criteria);
        return Ok(expenses);
    }
    
    [HttpPost]
    [Authorize]
    public IActionResult CreateExpense(ExpenseCreateDto incomeInfo)
    {
        _expenseService.CreateExpense(incomeInfo);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateIncome(ExpenseUpdateDto incomeInfo)
    {
        _expenseService.UpdateExpense(incomeInfo);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteIncome(int id)
    {
        _expenseService.DeleteExpense(id);
        return Ok();
    }
}
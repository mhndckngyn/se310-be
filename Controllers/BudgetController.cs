using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services;

namespace spendo_be.Controllers;

[Route("[controller]")]
[ApiController]
public class BudgetController : ControllerBase
{
    private IBudgetService _budgetService;

    public BudgetController(IBudgetService budgetService)
    {
        _budgetService = budgetService;
    }

    [Authorize]
    [HttpGet("{id:int}")]
    public IActionResult GetBudget([FromRoute] int id)
    {
        var budget = _budgetService.GetBudgetById(id);
        if (budget == null)
        {
            return NotFound();
        }
        
        return Ok(budget);
    }

    [Authorize]
    [HttpGet]
    public IActionResult GetBudgets()
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized();
        }
        
        var budgets = _budgetService.GetListBudgetByUser(userId);
        return Ok(budgets);
    }

    [Authorize]
    [HttpPost]
    public IActionResult CreateBudget([FromBody] BudgetCreateDto budgetInfo)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized();
        }
        
        var budget = _budgetService.CreateBudget(userId, budgetInfo);
        return CreatedAtAction(nameof(GetBudget), new { id = budget.Id }, budget);
    }

    [Authorize]
    [HttpPut("{id:int}")]
    public IActionResult UpdateBudget([FromRoute] int id, [FromBody] BudgetUpdateDto budgetInfo)
    {
        var updatedBudget = _budgetService.UpdateBudget(id, budgetInfo);
        if (updatedBudget == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }

    [Authorize]
    [HttpDelete("{id:int}")]
    public IActionResult DeleteBudget([FromRoute] int id)
    {
        var deletedBudget = _budgetService.DeleteBudget(id);
        if (deletedBudget == null)
        {
            return NotFound();
        }
        
        return NoContent();
    }
}
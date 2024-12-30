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

    [HttpGet]
    [Authorize]
    public IActionResult GetBudgets()
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        var budgets = _budgetService.GetListBudget(userId);
        return Ok(budgets);
    }

    [HttpPost]
    [Authorize]
    public IActionResult CreateBudget([FromBody] BudgetCreateDto budgetInfo)
    {
        var claimUserId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (!int.TryParse(claimUserId, out var userId))
        {
            return Unauthorized("Invalid or missing user ID.");
        }
        _budgetService.CreateBudget(budgetInfo, userId);
        return Ok();
    }

    [HttpPut]
    [Authorize]
    public IActionResult UpdateBudget([FromBody] BudgetUpdateDto budgetInfo)
    {
        _budgetService.UpdateBudget(budgetInfo);
        return Ok();
    }

    [HttpDelete]
    [Authorize]
    public IActionResult DeleteBudget(int id)
    {
        _budgetService.DeleteBudget(id);
        return Ok();
    }
}
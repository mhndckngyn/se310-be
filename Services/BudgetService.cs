using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class BudgetService : IBudgetService
{
    private readonly SpendoContext _context = new();

    public Budget CreateBudget(BudgetCreateDto budgetInfo, int userId)
    {
        var budget = new Budget
        {
            Name = budgetInfo.Name,
            Startdate = budgetInfo.StartDate,
            Enddate = budgetInfo.StartDate.AddDays(budgetInfo.Period),
            Period = budgetInfo.Period,
            Budgetlimit = budgetInfo.BudgetLimit,
            Categoryid = budgetInfo.CategoryId,
        };
        _context.Budgets.Add(budget);
        _context.SaveChanges();
        return budget;
    }
    
    public List<BudgetDto> GetListBudget(int userId)
    {
        var budgets = _context.Budgets.Where(b => b.Userid == userId)
            .Select(b => new BudgetDto
            {
                Id = b.Id,
                Name = b.Name,
                StartDate = b.Startdate,
                EndDate = b.Enddate,
                Current = b.Current,
                BudgetLimit = b.Budgetlimit
            }).ToList();
        return budgets;
    }

    public Budget UpdateBudget(BudgetUpdateDto budgetInfo)
    {
        var budget = _context.Budgets.FirstOrDefault(b => b.Id == budgetInfo.Id);
        budget.Name = budgetInfo.Name;
        _context.Budgets.Update(budget);
        _context.SaveChanges();
        return budget;
    }

    public Budget DeleteBudget(int id)
    {
        var budget = _context.Budgets.Find(id);
        if (budget != null) _context.Budgets.Remove(budget);
        return budget;
    }
}
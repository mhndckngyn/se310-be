using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;

namespace spendo_be.Services;

public class BudgetService : IBudgetService
{
    private readonly SpendoContext _context = new();

    public BudgetDto? GetBudgetById(int id)
    {
        var budget = _context.Budgets.Where(a => a.Id == id)
            .Select(a => new BudgetDto
            {
                Id = a.Id,
                Name = a.Name,
                StartDate = a.Startdate,
                EndDate = a.Enddate,
                Current = a.Current,
                BudgetLimit = a.Budgetlimit
            })
            .FirstOrDefault();
        
        return budget;
    }

    public Budget CreateBudget(int userId, BudgetCreateDto budgetInfo)
    {
        var budget = new Budget
        {
            Name = budgetInfo.Name,
            Startdate = budgetInfo.StartDate,
            Enddate = budgetInfo.StartDate.AddDays(budgetInfo.Period),
            Period = budgetInfo.Period,
            Budgetlimit = budgetInfo.BudgetLimit,
            Categoryid = budgetInfo.CategoryId,
            Userid = userId
        };
        _context.Budgets.Add(budget);
        _context.SaveChanges();
        return budget;
    }
    
    public List<BudgetDto> GetListBudgetByUser(int userId, int? categoryId)
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

    public List<BudgetDto> GetListBudgetByCategory(int userId, int categoryId)
    {
        throw new NotImplementedException();
    }

    public Budget? UpdateBudget(int id, BudgetUpdateDto budgetInfo)
    {
        var budget = _context.Budgets.Find(id);
        if (budget == null)
        {
            return null;
        }
        
        budget.Name = budgetInfo.Name;
        _context.Budgets.Update(budget);
        _context.SaveChanges();
        return budget;
    }

    public Budget? DeleteBudget(int id)
    {
        var budget = _context.Budgets.Find(id);
        if (budget == null)
        {
            return null;
        }
        
        _context.Budgets.Remove(budget);
        _context.SaveChanges();
        return budget;
    }
}
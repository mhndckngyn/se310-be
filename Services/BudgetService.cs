using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class BudgetService : IBudgetService
{
    private readonly SpendoContext _context = new();

    public Budget CreateBudget(Budget budget)
    {
        _context.Budgets.Add(budget);
        _context.SaveChanges();
        return budget;
    }
    
    public async Task<List<Budget>> GetListBudget(BudgetQueryCriteria criteria)
    {
        var query = _context.Budgets.AsQueryable();
        
        query = query.Where(b => b.Userid == criteria.UserId);
        
        if (criteria.IsOnlyExceeded)
        {
            query = query.Where(b => b.Current > b.Budgetlimit);
        }
        
        query = query.OrderBy(b => b.Id);
        return await query.ToListAsync();
    }

    public Budget UpdateBudget(Budget budget)
    {
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
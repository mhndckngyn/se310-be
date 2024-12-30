using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class ExpenseService : IExpenseService
{
    private readonly SpendoContext _context = new();
    
    public Expense CreateExpense(Expense expense)
    {
        _context.Expenses.Add(expense);
        _context.SaveChanges();
        return expense;
    }

    public async Task<List<Expense>> GetListExpense(RecordQueryCriteria criteria)
    {
        var query = _context.Expenses.AsQueryable();

        if (criteria.AccountIds is null)
        {
            query = query.Where(e => e.Account.Userid == criteria.UserId);
        }
        else if (criteria.AccountIds.Length > 0)
        {
            query = query.Where(e => criteria.AccountIds.Contains(e.Accountid));
        }

        if (criteria.CategoryIds != null && criteria.CategoryIds.Length > 0)
        {
            query = query.Where(e => e.Categoryid.HasValue && criteria.CategoryIds.Contains(e.Categoryid.Value));
        }

        if (criteria.StartDate.HasValue)
        {
            query = query.Where(e => e.Createdat >= criteria.StartDate);
        }
        
        if (criteria.EndDate.HasValue)
        {
            query = query.Where(e => e.Createdat >= criteria.EndDate);
        }

        query = query.OrderByDescending(e => e.Createdat);
        return await query.ToListAsync();
    }

    public Expense UpdateExpense(Expense expense)
    {
        _context.Expenses.Update(expense);
        _context.SaveChanges();
        return expense;
    }

    public Expense DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense != null) _context.Expenses.Remove(expense);
        _context.SaveChanges();
        return expense;
    }
}
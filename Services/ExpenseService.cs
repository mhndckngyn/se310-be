using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class ExpenseService : IExpenseService
{
    private readonly SpendoContext _context = new();

    public Expense? GetExpenseById(int id)
    {
        var expense = _context.Expenses.Find(id);
        return expense;
    }
    
    public Expense CreateExpense(ExpenseCreateDto expenseInfo)
    {
        var expense = new Expense
        {
            Title = expenseInfo.Title,
            Description = expenseInfo.Description,
            Amount = expenseInfo.Amount,
            Date = expenseInfo.Date,
            Accountid = expenseInfo.AccountId,
            Categoryid = expenseInfo.CategoryId
        };
        
        _context.Expenses.Add(expense);
        _context.SaveChanges();
        return expense;
    }

    public List<Expense> GetListExpenseByCriteria(RecordQueryCriteria criteria)
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
            query = query.Where(e => e.Date >= criteria.StartDate);
        }
        
        if (criteria.EndDate.HasValue)
        {
            query = query.Where(e => e.Date >= criteria.EndDate);
        }

        query = query.OrderByDescending(e => e.Date);
        return query.ToList();
    }

    public Expense? UpdateExpense(ExpenseUpdateDto expenseInfo)
    {
        var expense = _context.Expenses.Find(expenseInfo.Id);

        if (expense == null)
        {
            return null;
        }
        
        expense.Title = expenseInfo.Title;
        expense.Description = expenseInfo.Description;
        expense.Categoryid = expenseInfo.CategoryId;
        expense.Amount = expenseInfo.Amount;
        expense.Date = expenseInfo.Date;
        expense.Accountid = expenseInfo.AccountId;
        
        _context.Expenses.Update(expense);
        _context.SaveChanges();
        return expense;
    }

    public Expense? DeleteExpense(int id)
    {
        var expense = _context.Expenses.Find(id);
        if (expense == null)
        {
            return null;
        }
        
        _context.Expenses.Remove(expense);
        _context.SaveChanges();
        return expense;
    }
}
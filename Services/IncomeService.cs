using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class IncomeService : IIncomeService
{
    private readonly SpendoContext _context = new();

    public Income CreateIncome(Income income)
    {
        _context.Incomes.Add(income);
        _context.SaveChanges();
        return income;
    }

    public async Task<List<Income>> GetListIncomeByCriteria(RecordQueryCriteria criteria)
    {
        var query = _context.Incomes.AsQueryable();

        if (criteria.AccountIds is null)
        {
            query = query.Where(i => i.Account.Userid == criteria.UserId);
        }
        else if (criteria.AccountIds.Length > 0)
        {
            query = query.Where(i => criteria.AccountIds.Contains(i.Accountid));
        }

        if (criteria.CategoryIds != null && criteria.CategoryIds.Length > 0)
        {
            query = query.Where(i => i.Categoryid.HasValue && criteria.CategoryIds.Contains(i.Categoryid.Value));
        }

        if (criteria.StartDate.HasValue)
        {
            query = query.Where(i => i.Createdat >= criteria.StartDate);
        }

        if (criteria.EndDate.HasValue)
        {
            query = query.Where(i => i.Createdat <= criteria.EndDate);
        }

        query = query.OrderByDescending(i => i.Createdat);
        return await query.ToListAsync();
    }

    public Income UpdateIncome(Income income)
    {
        _context.Incomes.Update(income);
        _context.SaveChanges();
        return income;
    }

    public Income DeleteIncome(int id)
    {
        var income = _context.Incomes.Find(id);
        if (income != null) _context.Incomes.Remove(income);
        _context.SaveChanges();
        return income;
    }
}
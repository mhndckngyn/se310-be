using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class IncomeService : IIncomeService
{
    private readonly SpendoContext _context = new();

    public Income CreateIncome(IncomeCreateDto incomeInfo)
    {
        var income = new Income
        {
            Title = incomeInfo.Title,
            Description = incomeInfo.Description,
            Amount = incomeInfo.Amount,
            Date = incomeInfo.Date,
            Accountid = incomeInfo.AccountId,
            Categoryid = incomeInfo.CategoryId
        };
        
        _context.Incomes.Add(income);
        _context.SaveChanges();
        return income;
    }

    public List<Income> GetListIncomeByCriteria(RecordQueryCriteria criteria)
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
            query = query.Where(i => i.Date >= criteria.StartDate);
        }

        if (criteria.EndDate.HasValue)
        {
            query = query.Where(i => i.Date <= criteria.EndDate);
        }

        query = query.OrderByDescending(i => i.Date);
        return query.ToList();
    }

    public Income UpdateIncome(IncomeUpdateDto incomeInfo)
    {
        var income = _context.Incomes.FirstOrDefault(i => i.Id == incomeInfo.Id);
        if (incomeInfo.Title != income.Title)
        {
            income.Title = incomeInfo.Title;
        }

        if (incomeInfo.Description != income.Description)
        {
            income.Description = incomeInfo.Description;
        }

        if (incomeInfo.CategoryId != income.Categoryid)
        {
            income.Categoryid = incomeInfo.CategoryId;
        }
        
        income.Amount = incomeInfo.Amount;
        income.Date = incomeInfo.Date;
        income.Accountid = incomeInfo.AccountId;
        
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
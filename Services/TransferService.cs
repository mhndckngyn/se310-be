using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class TransferService : ITransferService
{
    private readonly SpendoContext _context = new();
    
    public Transfer CreateTransfer(Transfer transfer)
    {
        _context.Transfers.Add(transfer);
        _context.SaveChanges();
        return transfer;
    }

    public async Task<List<Transfer>> GetListTransferByCriteria(RecordQueryCriteria criteria)
    {
        var query = _context.Transfers.AsQueryable();

        if (criteria.AccountIds is null)
        {
            query = query.Where(t => t.Sourceaccount.Userid == criteria.UserId || t.Targetaccount.Userid == criteria.UserId);
        } else if (criteria.AccountIds.Length > 0)
        {
            query = query.Where(t => criteria.AccountIds.Contains(t.Sourceaccount.Userid) || criteria.AccountIds.Contains(t.Targetaccount.Userid));
        }

        if (criteria.CategoryIds != null && criteria.CategoryIds.Length > 0)
        {
            query = query.Where(t => t.Categoryid.HasValue && criteria.CategoryIds.Contains(t.Categoryid.Value));
        }

        if (criteria.StartDate.HasValue)
        {
            query = query.Where(t => t.Createdat >= criteria.StartDate);
        }

        if (criteria.EndDate.HasValue)
        {
            query = query.Where(t => t.Createdat <= criteria.EndDate);
        }
        
        query = query.OrderByDescending(t => t.Createdat);
        return await query.ToListAsync();
    }

    public Transfer UpdateTransfer(Transfer transfer)
    {
        _context.Transfers.Update(transfer);
        _context.SaveChanges();
        return transfer;
    }

    public Transfer DeleteTransfer(int id)
    {
        var transfer = _context.Transfers.Find(id);
        if (transfer != null) _context.Transfers.Remove(transfer);
        _context.SaveChanges();
        return transfer;
    }
}
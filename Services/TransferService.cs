using Microsoft.EntityFrameworkCore;
using spendo_be.Context;
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public class TransferService : ITransferService
{
    private readonly SpendoContext _context = new();
    
    public Transfer CreateTransfer(TransferCreateDto transferInfo)
    {
        var transfer = new Transfer
        {
            Title = transferInfo.Title,
            Description = transferInfo.Description,
            Amount = transferInfo.Amount,
            Date = transferInfo.Date,
            Sourceaccountid = transferInfo.SourceAccountId,
            Targetaccountid = transferInfo.TargetAccountId,
            Categoryid = transferInfo.CategoryId,
        };
        _context.Transfers.Add(transfer);
        _context.SaveChanges();
        return transfer;
    }

    public List<Transfer> GetListTransferByCriteria(RecordQueryCriteria criteria)
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
            query = query.Where(t => t.Date >= criteria.StartDate);
        }

        if (criteria.EndDate.HasValue)
        {
            query = query.Where(t => t.Date <= criteria.EndDate);
        }
        
        query = query.OrderByDescending(t => t.Date);
        return query.ToList();
    }

    public Transfer UpdateTransfer(TransferUpdateDto transferInfo)
    {
        var transfer = _context.Transfers.FirstOrDefault(t => t.Id == transferInfo.Id);

        if (transferInfo.Title != transfer.Title)
        {
            transfer.Title = transferInfo.Title;
        }
        
        if (transferInfo.Description != null)
        {
            transfer.Description = transferInfo.Description;
        }

        if (transferInfo.CategoryId != null)
        {
            transfer.Categoryid = transferInfo.CategoryId;
        }
        
        transfer.Amount = transferInfo.Amount;
        transfer.Date = transferInfo.Date;
        transfer.Sourceaccountid = transferInfo.SourceAccountId;
        transfer.Targetaccountid = transferInfo.TargetAccountId;
        
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
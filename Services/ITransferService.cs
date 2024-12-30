using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface ITransferService
{
    public Transfer CreateTransfer(Transfer transfer);
    public Task<List<Transfer>> GetListTransferByCriteria(RecordQueryCriteria criteria);
    public Transfer UpdateTransfer(Transfer transfer);
    public Transfer DeleteTransfer(int id);
}
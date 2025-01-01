using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface ITransferService
{
    public Transfer CreateTransfer(TransferCreateDto transferInfo);
    public Transfer? GetTransferById(int id);
    public List<Transfer> GetListTransferByCriteria(RecordQueryCriteria criteria);
    public Transfer? UpdateTransfer(int id, TransferUpdateDto transferInfo);
    public Transfer? DeleteTransfer(int id);
}
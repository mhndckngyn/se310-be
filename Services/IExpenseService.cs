using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IExpenseService
{
    public Expense CreateExpense(Expense expense);
    public Task<List<Expense>> GetListExpense(RecordQueryCriteria criteria);
    public Expense UpdateExpense(Expense expense);
    public Expense DeleteExpense(int id);
}
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IExpenseService
{
    public Expense? GetExpenseById(int id);
    public Expense CreateExpense(ExpenseCreateDto expenseInfo);
    public List<Expense> GetListExpenseByCriteria(RecordQueryCriteria criteria);
    public Expense? UpdateExpense(ExpenseUpdateDto expenseInfo);
    public Expense? DeleteExpense(int id);
}
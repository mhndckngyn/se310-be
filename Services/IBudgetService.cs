using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IBudgetService
{
    public Budget CreateBudget(Budget budget);
    public Task<List<Budget>> GetListBudget(BudgetQueryCriteria criteria);
    public Budget UpdateBudget(Budget budget);
    public Budget DeleteBudget(int id);
}
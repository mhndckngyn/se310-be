using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IBudgetService
{
    public Budget CreateBudget(BudgetCreateDto budgetInfo, int userId);
    public List<BudgetDto> GetListBudget(int userId);
    public Budget UpdateBudget(BudgetUpdateDto budgetInfo);
    public Budget DeleteBudget(int id);
}
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IBudgetService
{
    public BudgetDto? GetBudgetById(int id);
    public Budget CreateBudget(BudgetCreateDto budgetInfo);
    public List<BudgetDto> GetListBudgetByUser(int userId);
    public Budget? UpdateBudget(BudgetUpdateDto budgetInfo);
    public Budget? DeleteBudget(int id);
}
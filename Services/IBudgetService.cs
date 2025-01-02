using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IBudgetService
{
    public BudgetDto? GetBudgetById(int id);
    public Budget CreateBudget(int userId, BudgetCreateDto budgetInfo);
    public List<BudgetDto> GetListBudgetByUser(int userId);
    public Budget? UpdateBudget(int id, BudgetUpdateDto budgetInfo);
    public Budget? DeleteBudget(int id);
}
using spendo_be.Models;
using spendo_be.Models.DTO;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IIncomeService
{
    public Income CreateIncome(IncomeCreateDto incomeInfo);
    public Income? GetIncomeById(int id);
    public List<Income> GetListIncomeByCriteria(RecordQueryCriteria criteria);
    public Income? UpdateIncome(int id, IncomeUpdateDto incomeInfo);
    public Income? DeleteIncome(int id);
}
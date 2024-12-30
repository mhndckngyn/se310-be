using spendo_be.Models;
using spendo_be.Services.QueryCriteria;

namespace spendo_be.Services;

public interface IIncomeService
{
    public Income CreateIncome(Income income);
    public Task<List<Income>> GetListIncomeByCriteria(RecordQueryCriteria criteria);
    public Income UpdateIncome(Income income);
    public Income DeleteIncome(int id);
}
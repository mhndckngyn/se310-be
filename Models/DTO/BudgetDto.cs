namespace spendo_be.Models.DTO;

public record BudgetDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public DateOnly StartDate { get; init; }
    public DateOnly EndDate { get; init; }
    public decimal Current { get; init; }
    public decimal BudgetLimit { get; init; }
    
    public int? CategoryId { get; init; }
}
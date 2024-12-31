namespace spendo_be.Models.DTO;

public record BudgetCreateDto
{
    public string Name { get; init; }
    public DateOnly StartDate { get; init; }
    public int Period { get; init; }
    public decimal BudgetLimit { get; init; }
    public int? CategoryId { get; init; }
    public int UserId { get; set; }
};
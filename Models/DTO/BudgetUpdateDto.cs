namespace spendo_be.Models.DTO;

public record BudgetUpdateDto
{
    public int Id { get; init; }
    public string Name { get; init; }
};
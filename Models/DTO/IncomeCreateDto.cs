namespace spendo_be.Models.DTO;

public record IncomeCreateDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public int AccountId { get; init; }
    public int CategoryId { get; init; }
    public DateTime Date { get; init; }
}
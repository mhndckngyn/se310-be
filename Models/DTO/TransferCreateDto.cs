namespace spendo_be.Models.DTO;

public record TransferCreateDto
{
    public string? Title { get; init; }
    public string? Description { get; init; }
    public decimal Amount { get; init; }
    public int SourceAccountId { get; init; }
    public int TargetAccountId { get; init; }
    public int? CategoryId { get; init; }
    public DateTime Date { get; init; }
};
namespace spendo_be.Services.QueryCriteria;

public record BudgetQueryCriteria
{
    public int UserId { get; set; }
    public bool IsOnlyExceeded { get; init; } = false;
}
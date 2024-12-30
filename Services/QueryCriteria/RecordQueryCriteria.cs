namespace spendo_be.Services.QueryCriteria;

public record RecordQueryCriteria
{
    public int UserId { get; set; }
    public int[]? AccountIds { get; init; }
    public int[]? CategoryIds { get; init; }
    public DateTime? StartDate { get; init; }
    public DateTime? EndDate { get; init; }
};
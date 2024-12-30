namespace spendo_be.Models.DTO;

public record AccountUpdateDto
{
    public int Id { get; init; }
    public string? Name { get; init; }
}
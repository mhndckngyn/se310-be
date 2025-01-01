namespace spendo_be.Models.DTO;

public record AccountCreateDto
{
    public string Name { get; init; }

    public decimal Balance { get; init; }
}
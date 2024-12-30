namespace spendo_be.Models.DTO;

public record AccountDto
{
    public int Id { get; init; }
    public string Name { get; init; }
    public decimal Balance { get; init; }
};
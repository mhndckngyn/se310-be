namespace spendo_be.Controllers;

public record UserDto
{
    public string Email { get; init; }
    public string Name { get; init; }
    public int CurrencyId { get; init; }
};
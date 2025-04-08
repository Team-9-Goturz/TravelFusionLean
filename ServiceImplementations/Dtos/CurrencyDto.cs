namespace Shared.Dtos;

/// <summary>
/// DTO for valutaoplysninger, brugt til visning af symbol.
/// </summary>
public class CurrencyDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string Symbol { get; set; } = string.Empty;
}
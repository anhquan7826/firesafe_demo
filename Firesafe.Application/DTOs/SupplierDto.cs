namespace Application.DTOs;

public class SupplierShortDto
{
    public required Guid SupplierId { get; init; }
    public string? Name { get; init; }
    public string? Avatar { get; set; }
    public string? Banner { get; set; }
}

public class SupplierDto : SupplierShortDto
{
    public string? Description { get; init; }
    public required DateOnly EstablishedAt { get; init; }
    public short Rating { get; init; }
    public string? Address { get; init; }
}
using Firesafe.Domain.Core.Command;

namespace Firesafe.Domain.Commands;

public class AddNewProductCommand : Command<bool>
{
    public required Guid ProductId { get; set; }
    public required Guid SupplierId { get; set; }
    public required string Name { get; init; }
    public required string OriginId { get; init; }
    public required string Brand { get; init; }
    public required string Model { get; init; }
    public required string ShortDescription { get; init; }
    public required bool Available { get; init; }
    public required short Warranty { get; init; }
    public required string Packaging { get; init; }
    public required short ShippingTime { get; init; }
    public required string HasSample { get; init; }
    public required string ProductionRate { get; init; }
    public required string Accessories { get; init; }
    public required string PostSupport { get; init; }
    public string? Shape { get; init; }
    public string? Color { get; init; }
    public string? Material { get; init; }
    public int? Weight { get; init; }
    public int? Volume { get; init; }
    public string? Structure { get; init; }
    public string? FireResistant { get; init; }
    public string? WaterResistant { get; init; }
    public string? Applications { get; init; }
    public string? AdditionalSpecification { get; init; }
    public required string Faq { get; init; }
    public int? Price { get; init; }
    public int? PriceMax { init; get; }
    public int? PriceMin { get; init; }
    public required List<string> Categories { get; init; }
    public required List<(Guid, string)> Certificates { get; set; }
    public required List<Guid> ImageIds { get; set; }
}
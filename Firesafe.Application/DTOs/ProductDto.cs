namespace Application.DTOs;

public class ProductShortDto
{
    public required Guid ProductId { get; init; }
    public required string Name { get; init; }
    public required string Thumbnail { get; set; }
    public required int Rating { get; init; }
    public int? Price { get; init; }
    public int? PriceMax { get; init; }
    public int? PriceMin { get; init; }
}

public class ProductDto : ProductShortDto
{
    public class ProductCertificateDto
    {
        public required string Name { get; init; }
        public required string Image { get; init; }
    }

    public required string OriginId { get; init; }

    public required string Brand { get; init; }

    public required string Model { get; init; }

    public required string ShortDescription { get; init; }

    public required bool Available { get; init; }

    public required List<ProductCertificateDto> Certificates { get; set; } = null!;

    public required short Warranty { get; init; }

    public required string Packaging { get; init; }

    public required short ShippingTime { get; init; }

    public required string HasSample { get; init; }

    public required string ProductionRate { get; init; }

    public required string Accessories { get; init; }

    public required string PostSupport { get; init; }

    public required string Faq { get; init; }

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

    public List<string> Images { get; set; } = new();
    public List<string> Categories { get; set; } = new();
}
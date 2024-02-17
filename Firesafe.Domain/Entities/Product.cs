using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Product
{
    public Guid ProductId { get; init; }

    // Basic Info
    [MaxLength(50)] public required string Name { get; init; }

    [MaxLength(2)] public string OriginId { get; init; } = "vn";

    [MaxLength(100)] public required string Brand { get; init; }

    [MaxLength(100)] public required string Model { get; init; }

    [MaxLength(10000)] public required string ShortDescription { get; init; }

    public required bool Available { get; init; }

    public required List<ProductCertificate> ProductCertificates { get; set; } = null!;

    public required short Warranty { get; init; }

    [MaxLength(500)] public required string Packaging { get; init; }

    public required short ShippingTime { get; init; }

    [MaxLength(500)] public required string HasSample { get; init; }

    [MaxLength(500)] public required string ProductionRate { get; init; }

    [MaxLength(500)] public string? Accessories { get; init; }

    [MaxLength(500)] public string? PostSupport { get; init; }

    public int? Price { get; init; }

    public int? PriceMax { get; init; }

    public int? PriceMin { get; init; }

    public short Rating { get; init; }

    // public required int ProvinceOriginId { get; init; }
    //
    // public required int DistrictOriginId { get; init; }
    //
    // public required int WardOriginId { get; init; }

    [MaxLength(10000)] public string? Faq { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    // Specification

    [MaxLength(500)] public string? Shape { get; init; }

    [MaxLength(500)] public string? Color { get; init; }

    [MaxLength(500)] public string? Material { get; init; }

    public int? Weight { get; init; }

    public int? Volume { get; init; }

    [MaxLength(1000)] public string? Structure { get; init; }

    [MaxLength(1000)] public string? FireResistant { get; init; }

    [MaxLength(1000)] public string? WaterResistant { get; init; }

    [MaxLength(1000)] public string? Applications { get; init; }

    // Additional specification
    [MaxLength(8000)] public string? AdditionalSpecification { get; init; }

    // Foreign Keys
    public Guid SupplierId { get; init; }

    // Navigations 
    public Supplier Supplier { get; init; } = null!;

    public Origin Origin { get; init; } = null!;

    public List<ProductImage> ProductImages { get; set; } = null!;

    public List<Category> Categories { get; set; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.ProductId);
        builder.HasIndex(p => p.SupplierId);

        builder
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity<ProductCategory>();

        builder
            .HasOne(p => p.Origin)
            .WithMany(o => o.Products)
            .HasForeignKey(p => p.OriginId)
            .OnDelete(DeleteBehavior.NoAction)
            .IsRequired();
    }
}
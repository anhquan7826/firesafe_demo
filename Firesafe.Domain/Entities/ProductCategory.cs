using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class ProductCategory
{
    public Guid ProductId { get; init; }

    [MaxLength(50)] public required string CategoryId { get; init; }

    // Navigations
    public Product Product { get; init; } = null!;

    public Category Category { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<ProductCategory> builder)
    {
        builder.HasKey(pc => new { pc.ProductId, pc.CategoryId });

        // builder
        //     .HasOne(pc => pc.Product)
        //     .WithMany()
        //     .HasForeignKey(pc => pc.ProductId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder
        //     .HasOne(pc => pc.Category)
        //     .WithMany()
        //     .HasForeignKey(pc => pc.CategoryId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}
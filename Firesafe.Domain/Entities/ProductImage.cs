using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class ProductImage
{
    public Guid ProductImageId { get; init; }

    public required short Order { get; init; }

    // Foreign Keys
    public Guid ProductId { get; init; }


    public static void ConfigureRelationship(EntityTypeBuilder<ProductImage> builder)
    {
        builder.HasKey(pi => pi.ProductImageId);
        builder.HasIndex(pi => pi.ProductId);
        builder
            .HasOne<Product>()
            .WithMany(p => p.ProductImages)
            .HasForeignKey(pi => pi.ProductId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class ProductCertificate
{
    public Guid ProductCertificateId { get; init; }

    [MaxLength(500)] public required string Name { get; init; }

    public Guid ProductId { get; init; }

    // Navigations
    public Product Product { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<ProductCertificate> builder)
    {
        builder.HasKey(pc => pc.ProductCertificateId);
        builder
            .HasOne(pc => pc.Product)
            .WithMany(p => p.ProductCertificates)
            .HasForeignKey(pc => pc.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
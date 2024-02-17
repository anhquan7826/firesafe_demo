using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class SupplierReview
{
    public short Rating { get; init; } = 5;

    [MaxLength(500)] public string? Review { get; init; }

    // Foreign Keys
    public Guid UserId { get; init; }

    public Guid SupplierId { get; init; }

    // Navigations
    public User User { get; init; } = null!;

    public Supplier Supplier { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<SupplierReview> builder)
    {
        builder.HasKey(sr => new { sr.UserId, sr.SupplierId });
        builder.HasIndex(sr => sr.UserId);
        builder.HasIndex(sr => sr.SupplierId);
        builder
            .HasOne(sr => sr.User)
            .WithMany(u => u.SupplierReviews)
            .HasForeignKey(pr => pr.UserId)
            .IsRequired();
    }
}
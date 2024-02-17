using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class ProductReview
{
    public short Rating { get; init; } = 5;

    [MaxLength(1000)] public string? Review { get; init; }

    // Foreign Keys
    public Guid ProductId { get; init; }

    public Guid UserId { get; init; }

    // Navigations
    public Product Product { get; init; } = null!;

    public User User { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<ProductReview> builder)
    {
        builder.HasKey(pr => new { pr.ProductId, pr.UserId });
        builder.HasIndex(pr => pr.ProductId);
        builder.HasIndex(pr => pr.UserId);

        builder
            .HasOne(pr => pr.User)
            .WithMany(b => b.ProductReviews)
            .HasForeignKey(pr => pr.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne(pr => pr.Product)
            .WithMany()
            .HasForeignKey(pr => pr.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
    }
}
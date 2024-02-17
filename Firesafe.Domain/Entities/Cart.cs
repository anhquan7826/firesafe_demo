using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Cart
{
    [Required] public short Quantity { get; init; } = 1;

    [Required] public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    // Foreign Keys
    public Guid UserId { get; init; }

    public Guid ProductId { get; init; }

    // Navigations
    public Product Product { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Cart> builder)
    {
        builder.HasKey(c => new { c.UserId, c.ProductId });

        builder
            .HasOne<User>()
            .WithMany(b => b.Carts)
            .HasForeignKey(it => it.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();

        builder
            .HasOne<Product>(c => c.Product)
            .WithOne()
            .HasForeignKey<Cart>(c => c.ProductId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
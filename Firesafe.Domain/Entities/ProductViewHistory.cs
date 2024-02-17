using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class ProductViewHistory
{
    public required Guid UserId { get; init; }
    public required Guid ProductId { get; init; }
    
    public static void ConfigureRelationship(EntityTypeBuilder<ProductViewHistory> builder)
    {
        builder.HasKey(pi => new { pi.UserId, pi.ProductId });
        builder.HasIndex(pi => pi.ProductId);
        builder.HasIndex(pi => pi.UserId);
    }
}
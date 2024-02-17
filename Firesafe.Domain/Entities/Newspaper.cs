using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Newspaper
{
    public Guid NewspaperId { get; init; }
    
    [MaxLength(1000)] public required string Title { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public DateTime EditedAt { get; init; } = DateTime.UtcNow;

    // Navigations
    public List<NewspaperImage> NewspaperImages { get; init; } = null!;

    public List<NewspaperCategory> NewspaperCategories { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Newspaper> builder)
    {
        builder.HasKey(n => n.NewspaperId);

        builder
            .HasMany(n => n.NewspaperCategories)
            .WithMany(nc => nc.Newspapers)
            .UsingEntity<NewspaperCategoryJunction>();
    }
}
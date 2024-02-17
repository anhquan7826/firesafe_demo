using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class NewspaperImage
{
    public Guid NewspaperImageId { get; init; }
    
    // Foreign Keys
    public Guid NewspaperId { get; init; }
    
    public static void ConfigureRelationship(EntityTypeBuilder<NewspaperImage> builder)
    {
        builder.HasKey(n => n.NewspaperImageId);
        builder.HasIndex(n => n.NewspaperId);
        builder
            .HasOne<Newspaper>()
            .WithMany(n => n.NewspaperImages)
            .HasForeignKey(ni => ni.NewspaperId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
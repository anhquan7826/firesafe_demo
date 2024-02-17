using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class NewspaperCategoryJunction
{
    public Guid NewspaperId { get; set; }

    [MaxLength(50)] public required string NewspaperCategoryId { get; init; }
    
    // Navigations
    public Newspaper Newspaper { get; init; } = null!;

    public NewspaperCategory NewspaperCategory { get; init; } = null!;
    
    public static void ConfigureRelationship(EntityTypeBuilder<NewspaperCategoryJunction> builder)
    {
        builder.HasKey(ncj => new { ncj.NewspaperId, ncj.NewspaperCategoryId });

        // builder
        //     .HasOne(ncj => ncj.Newspaper)
        //     .WithMany()
        //     .HasForeignKey(ncj => ncj.NewspaperId)
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // builder
        //     .HasOne(ncj => ncj.NewspaperCategory)
        //     .WithMany()
        //     .HasForeignKey(ncj => ncj.NewspaperCategoryId)
        //     .OnDelete(DeleteBehavior.Cascade);
    }
}
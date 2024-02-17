using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class NewspaperCategory
{
    [MaxLength(50)] public required string NewspaperCategoryId { get; init; }

    // Navigations
    public List<NewspaperCategoryJunction> NewspaperCategoryJunctions { get; init; } = null!;

    public List<Newspaper> Newspapers { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<NewspaperCategory> builder)
    {
        builder.HasKey(nc => nc.NewspaperCategoryId);
    }

    public static void ConfigureInitialData(EntityTypeBuilder<NewspaperCategory> builder)
    {
        builder.HasData(
            new NewspaperCategory
            {
                NewspaperCategoryId = "newspaper_category_product_category"
            },
            new NewspaperCategory
            {
                NewspaperCategoryId = "newspaper_category_field"
            }
        );
    }
}
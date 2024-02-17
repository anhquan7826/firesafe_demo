using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Category
{
    [MaxLength(50)] public required string CategoryId { get; init; }

    // Navigations
    public IEnumerable<ProductCategory> ProductCategories { get; init; } = null!;
    public IEnumerable<Product> Products { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Category> builder)
    {
        builder.HasKey(c => c.CategoryId);
    }

    public static void ConfigureInitialData(EntityTypeBuilder<Category> builder)
    {
        builder.HasData(
            new Category
            {
                CategoryId = "category_material"
            },
            new Category
            {
                CategoryId = "category_construction_material"
            },
            new Category
            {
                CategoryId = "category_paint"
            },
            new Category
            {
                CategoryId = "category_pipe_cable"
            },
            new Category
            {
                CategoryId = "category_electric_system"
            },
            new Category
            {
                CategoryId = "category_door"
            },
            new Category
            {
                CategoryId = "category_household_appliance"
            },
            new Category
            {
                CategoryId = "category_other"
            }
        );
    }
}
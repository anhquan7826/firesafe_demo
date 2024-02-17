using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class User
{
    public Guid UserId { get; init; }

    [MaxLength(50)] [Required] public required string FirebaseId { get; init; }
    
    [MaxLength(50)] public string? Name { get; set; }
    
    // Navigations
    public Supplier? Supplier { get; init; }

    public List<UserDevice> UserDevices { get; init; } = null!;

    public List<Role> Roles { get; set; } = null!;
    
    public IEnumerable<Cart> Carts { get; init; } = null!;

    public IEnumerable<ProductReview> ProductReviews { get; init; } = null!;

    public IEnumerable<SupplierReview> SupplierReviews { get; init; } = null!;
    
    public List<Product> FavoriteProduct { get; init; } = null!;

    public List<Product> ProductHistory { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(u => u.UserId);
        builder.HasIndex(u => u.FirebaseId).IsUnique();
        builder
            .HasMany(u => u.Roles)
            .WithMany()
            .UsingEntity<UserRole>();
        builder
            .HasMany(u => u.FavoriteProduct)
            .WithMany()
            .UsingEntity<ProductFavorite>();
        builder
            .HasMany(u => u.ProductHistory)
            .WithMany()
            .UsingEntity<ProductViewHistory>();
    }
}
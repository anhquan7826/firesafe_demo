using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Supplier
{
    public Guid SupplierId { get; init; }

    [Required] public required Guid UserId { get; init; }

    [MaxLength(50)] public string? Name { get; set; }

    [MaxLength(10000)] public string? Description { get; set; }

    public DateOnly EstablishedAt { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    public short Rating { get; init; }

    [MaxLength(200)] public string? Avatar { get; init; }

    [MaxLength(200)] public string? Banner { get; init; }

    // Foreign Keys
    [MaxLength(200)] public string? Address { get; set; }

    // Navigations
    public User User { get; init; } = null!;

    // public Province? Province { get; init; }

    public List<Product> Products { get; init; } = null!;

    public List<SupplierReview> SupplierReviews { get; init; } = null!;

    public List<SupplierContact> SupplierContacts { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Supplier> builder)
    {
        builder.HasKey(s => s.SupplierId);
        builder.HasIndex(s => s.UserId).IsUnique();
        builder
            .HasOne(s => s.User)
            .WithOne(u => u.Supplier)
            .HasForeignKey<Supplier>(s => s.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired(false);
        builder
            .HasMany(s => s.Products)
            .WithOne(p => p.Supplier)
            .HasForeignKey(p => p.SupplierId)
            .IsRequired();
        builder
            .HasMany(s => s.SupplierReviews)
            .WithOne(sr => sr.Supplier)
            .HasForeignKey(sr => sr.SupplierId)
            .IsRequired();
        builder
            .HasMany(s => s.SupplierContacts)
            .WithOne(sc => sc.Supplier)
            .HasForeignKey(sc => sc.SupplierId)
            .IsRequired();
        // builder
        //     .HasOne(s => s.Province)
        //     .WithMany()
        //     .HasForeignKey(s => s.ProvinceId)
        //     .OnDelete(DeleteBehavior.SetNull)
        //     .IsRequired(false);
    }
}
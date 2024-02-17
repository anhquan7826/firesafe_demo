using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Contact
{
    public Guid ContactId { get; init; }

    [MaxLength(15)] [Required] public required string PhoneNumber { get; init; }

    [MaxLength(20)] [Required] public required string City { get; init; }

    [MaxLength(20)] [Required] public required string District { get; init; }

    [MaxLength(20)] [Required] public required string Ward { get; init; }

    [MaxLength(50)] public string? AddressDetail { get; init; }

    // Navigations
    public IEnumerable<SupplierContact> SupplierContacts { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<Contact> builder)
    {
        builder.HasKey(c => c.ContactId);
        builder
            .HasMany(c => c.SupplierContacts)
            .WithOne(sc => sc.Contact)
            .HasForeignKey(sc => sc.ContactId)
            .IsRequired();
    }
}
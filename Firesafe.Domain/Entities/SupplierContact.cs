using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class SupplierContact
{
    public Guid ContactId { get; init; }

    public Guid SupplierId { get; init; }

    // Navigations
    public Contact Contact { get; init; } = null!;

    public Supplier Supplier { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<SupplierContact> builder)
    {
        builder.HasKey(sc => new { sc.ContactId, sc.SupplierId });
    }
}
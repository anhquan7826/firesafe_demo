using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities.EventStore;

public class ExceptionStore
{
    public Guid Id { get; init; }
    [MaxLength(10_000)] public required string Message { get; init; }
    [MaxLength(100_000)] public required string StackTrace { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;

    public static void ConfigureRelationship(EntityTypeBuilder<ExceptionStore> builder)
    {
        builder.HasKey(ee => ee.Id);
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class Role
{
    [MaxLength(10)] [Required] public required string Type { get; init; }

    public override string ToString()
    {
        return Type;
    }

    public static void ConfigureRelationship(EntityTypeBuilder<Role> builder)
    {
        builder.HasKey(r => r.Type);
    }

    public static void ConfigureInitialData(EntityTypeBuilder<Role> builder)
    {
        builder.HasData(
            new Role
            {
                Type = Roles.Supplier
            }
        );
    }
}

/// <summary>
///     Specify all available roles in the system.
/// </summary>
public static class Roles
{
    /// <summary>
    ///     Indicates that this user is a supplier.
    /// </summary>
    public const string Supplier = "supplier";

    public static IEnumerable<string> GetAll()
    {
        return [Supplier];
    }
}
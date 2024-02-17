using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class UserRole
{
    public Guid UserId { get; init; }
    [MaxLength(20)] public required string RoleType { get; init; }
    
    // Navigations
    public User User { get; init; } = null!;

    public Role Role { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<UserRole> builder)
    {
        builder.HasKey(ur => new {ur.UserId, ur.RoleType});
    }
}
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

public class UserDevice
{
    public Guid UserId { get; init; }

    [MaxLength(200)] public required string FcmToken { get; init; }

    public DateTime CreatedAt { get; init; } = DateTime.UtcNow;

    // Navigations
    public User User { get; init; } = null!;

    public static void ConfigureRelationship(EntityTypeBuilder<UserDevice> builder)
    {
        builder.HasKey(ud => new { ud.UserId, ud.FcmToken });
        builder
            .HasOne(ud => ud.User)
            .WithMany(u => u.UserDevices)
            .HasForeignKey(ud => ud.UserId)
            .OnDelete(DeleteBehavior.Cascade)
            .IsRequired();
    }
}
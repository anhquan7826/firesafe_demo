using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Firesafe.Domain.Entities;

// public class Buyer
// {
//     public Guid BuyerId { get; init; }
//
//     
//     
//     // Foreign Keys 
//     [Required] public required Guid UserId { get; init; }
//
//     // Navigations
//     public User User { get; init; } = null!;
//
//     
//
//     public static void ConfigureRelationship(EntityTypeBuilder<Buyer> builder)
//     {
//         builder.HasKey(b => b.BuyerId);
//         builder.HasIndex(b => b.UserId).IsUnique();
//
//         
//     }
// }
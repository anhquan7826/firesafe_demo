using System.Security.Claims;
using Infrastructure.Database;
using Microsoft.AspNetCore.Authentication;

namespace Infrastructure.Authorization;

public class IdClaimsTransformation(DatabaseContext dbContext) : IClaimsTransformation
{
    public Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
    {
        var firebaseId = principal.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
        if (firebaseId == null) return Task.FromResult(principal);
        var userId = dbContext.Users.FirstOrDefault(u => u.FirebaseId == firebaseId)?.UserId;
        if (userId == null) return Task.FromResult(principal);
        var claimsIdentity = new ClaimsIdentity();
        claimsIdentity.AddClaim(new Claim("UserId", userId.ToString()!));
        var supplierId = dbContext.Suppliers.FirstOrDefault(s => s.UserId == userId)?.SupplierId;
        if (supplierId != null) claimsIdentity.AddClaim(new Claim("SupplierId", supplierId.ToString()!));
        principal.AddIdentity(claimsIdentity);
        return Task.FromResult(principal);
    }
}
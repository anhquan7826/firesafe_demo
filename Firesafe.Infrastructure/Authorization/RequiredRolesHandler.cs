using Infrastructure.Database;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Authorization;

public class RequiredRoles(IEnumerable<string>? roles = null) : IAuthorizationRequirement
{
    public IEnumerable<string>? Roles { get; } = roles;
}

public class RequiredRolesHandler(DatabaseContext dbContext) : AuthorizationHandler<RequiredRoles>
{
    /// <summary>
    /// requirement.Roles is null: Allow all users (still require Firebase registration).
    /// requirement.Roles is empty: Only allow registered users.
    /// requirement.Roles is not empty: Require users with at least one role contained in the requirement.
    /// </summary>
    /// <param name="context"></param>
    /// <param name="requirement"></param>
    /// <returns></returns>
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, RequiredRoles requirement)
    {
        var fid = context.User.FindFirst(c => c.Type.Contains("nameidentifier"))?.Value;
        if (fid == null)
        {
            context.Fail();
            return Task.CompletedTask;
        }

        if (requirement.Roles == null)
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        var userRoles = dbContext
            .Users
            .Include(u => u.Roles)
            .FirstOrDefault(u => u.FirebaseId == fid)
            ?.Roles
            .Select(r => r.Type);

        if (userRoles == null)
        {
            context.Fail(new AuthorizationFailureReason(this, "User is not registered!"));
            return Task.CompletedTask;
        }

        if (!requirement.Roles.Any())
        {
            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        if (!userRoles.Intersect(requirement.Roles).Any())
        {
            context.Fail();
            return Task.CompletedTask;
        }

        context.Succeed(requirement);
        return Task.CompletedTask;
    }
}
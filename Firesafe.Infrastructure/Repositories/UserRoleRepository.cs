using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRoleRepository(DbContext dbContext) : Repository<UserRole>(dbContext), IUserRoleRepository
{
    public void SetUserRoles(Guid userId, List<string> roles)
    {
        var range = DbSet.Where(ur => ur.UserId == userId);
        DbSet.RemoveRange(range);
        foreach (var role in roles)
            DbSet.Add(new UserRole
            {
                UserId = userId,
                RoleType = role
            });
    }

    public void AddUserRole(Guid userId, List<string> roles)
    {
        var existRoles = DbSet.Where(ur => ur.UserId == userId).Select(ur => ur.RoleType).ToList();
        foreach (var role in roles.Except(existRoles))
            DbSet.Add(new UserRole
            {
                UserId = userId,
                RoleType = role
            });
    }

    public void RemoveUserRole(Guid userId, List<string> roles)
    {
        var range = DbSet.Where(ur => ur.UserId == userId).Where(ur => roles.Contains(ur.RoleType));
        DbSet.RemoveRange(range);
    }
}
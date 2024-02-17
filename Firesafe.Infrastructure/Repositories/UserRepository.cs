using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Infrastructure.Database;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class UserRepository(DatabaseContext dbContext) : Repository<User>(dbContext), IUserRepository
{
    public User? GetByFirebaseId(string id)
    {
        return DbSet.Include(u => u.Roles).FirstOrDefault(it => it.FirebaseId == id);
    }

    public User AddNewUser(string firebaseId)
    {
        var user = new User
        {
            FirebaseId = firebaseId
        };
        Add(user);
        return user;
    }

    public void SetRoles(Guid userId, List<string> roles)
    {
        var userRoles = dbContext.Set<UserRole>();
        userRoles.AddRange(roles.Select(r => new UserRole
        {
            UserId = userId,
            RoleType = r
        }));
    }

    public User? SetInfo(Guid userId, string? name)
    {
        var user = DbSet.Find(userId);
        if (user != null) user.Name = name;

        return user;
    }
}
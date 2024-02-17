using Firesafe.Domain.Entities;
using Firesafe.Domain.Repositories;
using Infrastructure.Database;

namespace Infrastructure.Repositories;

public class RoleRepository(DatabaseContext dbContext) : Repository<Role>(dbContext), IRoleRepository
{
    public IEnumerable<Role> GetRoles()
    {
        return GetAll();
    }
}
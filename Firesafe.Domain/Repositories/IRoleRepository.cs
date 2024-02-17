using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IRoleRepository : IRepository<Role>
{
    public IEnumerable<Role> GetRoles();
}
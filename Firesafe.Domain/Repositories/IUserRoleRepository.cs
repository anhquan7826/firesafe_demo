using Firesafe.Domain.Core.Repository;
using Firesafe.Domain.Entities;

namespace Firesafe.Domain.Repositories;

public interface IUserRoleRepository : IRepository<UserRole>
{
    public void SetUserRoles(Guid userId, List<string> roles);

    public void AddUserRole(Guid userId, List<string> roles);

    public void RemoveUserRole(Guid userId, List<string> roles);
}